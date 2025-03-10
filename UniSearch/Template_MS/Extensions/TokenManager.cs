using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using UniSearch.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace UniSearch.Extensions
{
    public class TokenManager
    {

        public static Dictionary<string, string> GeneratedTokens = new Dictionary<string, string>();
        private static string Secret = "J8c20cxkPZCC/0e0ZUcjrGocsk95gOAqjzJ09apAklM=";
        private static double TokenExpireTime = 10;

        public static string GenerateToken(UserLogin obj)
        {

            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                         new Claim("FirstName", string.Join(',',obj.FirstName)),
                         new Claim("LastName", string.Join(',',obj.LastName)),
                         new Claim("UserName", string.Join(',',obj.UserName)),
                         new Claim("Email", string.Join(',',obj.Email)),
                         new Claim("RoleId", string.Join(',',obj.RoleId)),
                         new Claim("LoginId", string.Join(',',obj.LoginId)),
                         new Claim("RoleName", string.Join(',',obj.UserRole.RoleName)),
                      }
                ),
                Issuer = "Remittance",
                Expires = DateTime.Now.AddMinutes(TokenExpireTime),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            string value = string.Empty;
            bool keyExists = GeneratedTokens.TryGetValue(obj.UserName, out value);

            if (keyExists)
            {
                GeneratedTokens.Remove(obj.UserName);
                GeneratedTokens.Add(obj.UserName, handler.WriteToken(token).ToString());
            }
            else
            {
                GeneratedTokens.Add(obj.UserName, handler.WriteToken(token).ToString());
            }

            return handler.WriteToken(token);
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static UserLogin ValidateToken(string token)
        {
            UserLogin obj = new UserLogin();
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }


            Claim firstName = identity.FindFirst("FirstName");
            Claim lastName = identity.FindFirst("LastName");
            Claim Username = identity.FindFirst("UserName");
            Claim Email = identity.FindFirst("Email");
            Claim Phone = identity.FindFirst("Phone");
            Claim RoleId = identity.FindFirst("RoleId");
            Claim loginId = identity.FindFirst("LoginId");
            Claim RoleName = identity.FindFirst("RoleName");
            Claim AuthId = identity.FindFirst("AuthId");
            Claim AuthName = identity.FindFirst("AuthName");
            Claim LoginId = identity.FindFirst("LoginId");
            Claim BranchId = identity.FindFirst("BranchId");
            Claim BranchCode = identity.FindFirst("BranchCode");
            Claim TellerAccount = identity.FindFirst("TellerAccount");
            Claim TokenExpiry = identity.FindFirst("TokenExpiry");
            Claim BankId = identity.FindFirst("BankId");
            obj.FirstName = firstName.Value;
            obj.LastName = lastName.Value;
            obj.UserName = Username.Value;
            obj.Email = Email.Value;
            obj.LoginId = new Guid(LoginId.Value);
            obj.RoleId = new Guid(RoleId.Value);
            obj.UserRole = new UserRole();
            obj.UserRole.RoleName = RoleName.Value;
            return obj;

        }
        public static bool isSessionExist(UserLogin userLogin)
        {
            string value = string.Empty;
            return GeneratedTokens.TryGetValue(userLogin.UserName, out value);
        }
        public static UserLogin GetValidateToken(HttpRequest httpRequest)
        {
            string value = string.Empty;
            if (!httpRequest.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = httpRequest.Headers["Authorization"];
            UserLogin claimDTO = null;
            string token = authHeader;

            if (token != null)
            {
                if (token.Contains("Bearer"))
                {
                    token = token.Replace("Bearer ", "");
                }
                var item = GeneratedTokens.FirstOrDefault(kvp => kvp.Value == token);
                //if (GeneratedTokens.TryGetValue(headerValues[2], out generatedToken))
                //{
                //    generatedToken=GeneratedTokens[headerValues[2]];
                //}
                //if (!item.Equals(default(KeyValuePair<string, string>)))
                //{
                //    GeneratedTokens.Remove(item.Key);
                //}

            }

            claimDTO = ValidateToken(token);
            if (claimDTO == null)
            {
                return null;
            }

            return claimDTO;
        }





        public static string RemoveToken(HttpRequest httpRequest)
        {
            UserLogin userLogin = GetValidateToken(httpRequest);
            var item = GeneratedTokens.FirstOrDefault(kvp => kvp.Key == userLogin.UserName);
            if (!item.Equals(default(KeyValuePair<string, string>)))
            {
                GeneratedTokens.Remove(item.Key);
                return null;
            }
            else
            {
                return null;
            }

        }


    }
}
