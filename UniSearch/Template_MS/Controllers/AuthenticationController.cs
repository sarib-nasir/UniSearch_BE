using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSearch.Extensions;
using UniSearch.Services.Contract;
using UniSearch.ViewModels;
using System;
using System.Threading.Tasks;

namespace UniSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ApiResponse _apiResponse;

        protected readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _apiResponse = new ApiResponse();
        }



        [HttpPost]
        [Route("Authenticate")]
        public async Task<ApiResponse> Authenticate([FromBody] LoginViewModel obj)
        {
            try
            {
                _apiResponse = await _authenticationService.Authenticate(obj);
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex.ToString());
            }

            return _apiResponse;
        }


        [HttpPost("Logout")]
        [Authorize]
        public ApiResponse Logout()
        {
            try
            {
                Response.Cookies.Delete("Authorization");
                string token = Request.Headers["Authorization"];
                if (token != null)
                {
                    TokenManager.RemoveToken(Request);
                    _apiResponse.statusCode = "00";
                }
                else
                {
                    _apiResponse.statusCode = "11";
                    _apiResponse.data = null;
                    return _apiResponse;
                }
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex.ToString());
            }
            return _apiResponse;
        }

        [HttpPost]
        [Route("RefreshToken")]
        [Authorize]
        public async Task<ApiResponse> RefreshToken()
        {
            var userLogin = TokenManager.GetValidateToken(Request);
            if (userLogin == null) return CustomStatusResponse.GetResponse(401);


            _apiResponse.statusCode = "00";
            _apiResponse.Token = TokenManager.GenerateToken(userLogin);


            return _apiResponse;
        }

        
    }
}
