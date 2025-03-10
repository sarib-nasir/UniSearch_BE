using UniSearch.Extensions;
using UniSearch.Models;
using UniSearch.Services.Base;
using UniSearch.Services.Contract;
using UniSearch.ViewModels;
using System;
using System.Threading.Tasks;

namespace UniSearch.Services.Provider
{
    public class AuthenticationService : Service<UserLogin>, IAuthenticationService
    {
        private ApiResponse _apiResponse;
        public AuthenticationService()
        {
            _apiResponse = new ApiResponse();
        }

        public async Task<ApiResponse> Authenticate(LoginViewModel obj)
        {
            CustomLogger.Debug("========= START Authenticate() ===========");
            try
            {
                
                var userDetail = GetFirst(x => x.UserName.ToUpper() == obj.Email.ToUpper() && x.isActive == true, x => x.UserRole);
                if (userDetail != null)
                {
                    _apiResponse.Token = TokenManager.GenerateToken(userDetail);
                    _apiResponse.message = "User Login Successfully.";
                    _apiResponse.statusCode = "00";
                }
                else
                {
                     _apiResponse.message = "Unable to connect with user";                   
                }
            }
            catch (Exception ex)
            {

                CustomLogger.WriteErrorLogToFile(ex);
            }

            return _apiResponse;
        }
    }
}
