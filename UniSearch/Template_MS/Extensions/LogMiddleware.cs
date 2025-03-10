using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UniSearch.Data;
using UniSearch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace UniSearch.Extensions
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        protected readonly ApplicationDbContext Db;
        public string bodyAsText = null;
        private readonly IMemoryCache _memoryCache;
        static string cacheKey = "PermissionList";
        private const string CorrelationIdHeader = "X-Correlation-ID";

        public LogMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IMemoryCache memoryCache)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LogMiddleware>();
            Db = new ApplicationDbContext();
            _memoryCache = memoryCache;
        }

        public async Task Invoke(HttpContext context)
        {
            UserLogin userLogin = null;
            bool isProcess = true;

            //Adding Correlation ID in Request Headers to trace reuqest flow in logs.
            #region EMBED CORRELATION ID IN HEADER

            string loginId = null;
            string email = null;
            string token = context.Request.Headers["Authorization"];
 
            if (!string.IsNullOrEmpty(token) && token != "null")
            {
                token = token.Replace("Bearer ", "");
                var simplePrinciple = TokenManager.GetPrincipal(token);
                if (simplePrinciple != null && simplePrinciple.Identity != null)
                {
                    var identity = simplePrinciple.Identity as ClaimsIdentity;

                    loginId = identity.FindFirst("LoginId")?.Value;
                    email = identity.FindFirst("Email")?.Value;
                }
                string correlationId = $"LOGIN ID: {loginId} EMAIL: {email} REQUEST ID: {Guid.NewGuid()}";
                context.Request.Headers.Append(CorrelationIdHeader, correlationId);
            }
            else
            {
                string correlationId = $"REQUEST ID: {Guid.NewGuid()}";
                context.Request.Headers.Append(CorrelationIdHeader, correlationId);
            }

            #endregion

            if (context.Request.Headers.ContainsKey("Authorization") && context.Request.Path.Value != "/api/Authentication/Logout" && context.Request.Path.Value != "/api/Authentication/Authenticate")
            {
                userLogin = TokenManager.GetValidateToken(context.Request);
                if (userLogin != null)
                {

                    isProcess = TokenManager.isSessionExist(userLogin);
                }
                else
                {
                    isProcess = false;
                }
         
            }

            if (isProcess && (context.Request.Path.Value.Contains("api") && userLogin != null ? GetApiList(userLogin.RoleId, context.Request.Path.Value):true))
            {
                GetApiPermissions();
                _logger.LogInformation(await FormatRequest(context));

                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);

                    _logger.LogInformation(await FormatResponse(context));
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            else if(!isProcess)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            }
        }

        private async Task<string> FormatRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            request.EnableBuffering();
            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            bodyAsText = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpContext context)
        {
            HttpResponse response = context.Response;
            HttpRequest request = context.Request;
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            await AddLogsAsync(request, bodyAsText, text);

            return $"Response {text}";
        }

        public async Task AddLogsAsync(HttpRequest request, string requestBody, string responseBody)
        {
            try
            {
                ApiResponse apiResponse = null;
                UserLogin userLogin = new UserLogin();
                if (request.Headers.ContainsKey("Authorization"))
                {
                    userLogin = TokenManager.GetValidateToken(request);
                }
                if (!string.IsNullOrEmpty(responseBody) && !responseBody.Contains("html"))
                {
                    apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                }
                else
                {
                    responseBody = null;
                }



                LOGS log = new LOGS();
                if (apiResponse != null)
                {
                    log.STATUS_CODE = apiResponse.statusCode;
                    log.API_REQUESTDATA = requestBody;
                    log.ACTION = apiResponse.message;
                    log.API_RESPONSEDATA = responseBody;
                    log.TYPE_ID = apiResponse.typeId;
                }
                else if (!string.IsNullOrEmpty(requestBody))
                {
                    JObject jObject = JObject.Parse(requestBody);
                    jObject.Remove("password");
                    log.API_REQUESTDATA = jObject.ToString();
                }
                if (apiResponse != null || !string.IsNullOrEmpty(requestBody))
                {
                    log.URL = request.Path;

                    log.INPUT_DATE = DateTime.Now;
                    log.INPUT_IP = request.HttpContext.Connection.RemoteIpAddress.ToString();
                    log.INPUT_MAC = SecurityLayer.GetClientMacAddress();
                    log.INPUT_BROWSER = SecurityLayer.GetWebBrowser() != null ? SecurityLayer.GetWebBrowser().Name : null;
                    log.INPUT_BRO_VERSION = SecurityLayer.GetWebBrowser() != null ? SecurityLayer.GetWebBrowser().Version.ToString() : null;
                    if (userLogin != null)
                    {
                        log.INPUT_BY = userLogin.LoginId;
                    }

                    Db.Set<LOGS>().Add(log);
                    int saved = Db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                CustomLogger.WriteErrorLogToFile(e);
            }

        }

        public bool GetApiList(Guid RoleId , string EndPoint)
        {
            
            
            var permissionDetail = GetApiPermissions().Where(x => x.RoleId == RoleId && x.ApiURL == EndPoint && x.IsActive == true).FirstOrDefault();
            if (permissionDetail != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ApiPermissions> GetApiPermissions()
        {
            List<ApiPermissions> permissionsList = null;
            if (_memoryCache.TryGetValue(cacheKey, out List<ApiPermissions> amlDetail))
            {
                permissionsList = _memoryCache.Get<List<ApiPermissions>>(cacheKey);
            }
            else
            {
                permissionsList = Db.Set<ApiPermissions>().Where(x => x.IsActive == true).ToList();
                _memoryCache.Set(cacheKey, permissionsList);
            }
            return permissionsList;
        }
    }
}
