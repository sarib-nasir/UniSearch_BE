using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSearch.Extensions;
using UniSearch.Services.Contract;
using UniSearch.ViewModels;
using System;

namespace UniSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRegistrationController : ControllerBase
    {
        private ApiResponse _apiResponse;

        protected readonly IUserRegistrationService _UserRegistrationService;
        public UserRegistrationController(IUserRegistrationService userRegistrationService)
        {
            _UserRegistrationService = userRegistrationService;
            _apiResponse = new ApiResponse();
        }

        [HttpPost]
        [Route("AddOrUpdateUserRegistration")]
        public ApiResponse AddOrUpdateUserRegistration([FromBody] UserRegistrationViewModel userRegistrationViewModel)
        {           
            try
            {
                _apiResponse = _UserRegistrationService.AddOrUpdatUserRegistration(userRegistrationViewModel);
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex.ToString());
            }

            return _apiResponse;
        }

        [HttpPost]
        [Route("GetUserRegistrationDetail")]
        public ApiResponse GetUserRegistrationDetail([FromBody] UserRegistrationViewModel userLoginDetail)
        {
            try
            {
                _apiResponse = _UserRegistrationService.GetUserRegistrationDetail(userLoginDetail);
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex.ToString());
            }
            return _apiResponse;
        }

        [HttpPost]
        [Route("DeleteUserDetail")]
        public ApiResponse DeleteUserDetail([FromBody] ActiveViewModel activeViewModel)
        {
            CustomLogger.Debug("================== API DELETEAGENTDETAIL() ===================");
            try
            {
                _apiResponse = _UserRegistrationService.DeleteUserDetail(activeViewModel);
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex.ToString());
            }

            return _apiResponse;
        }
    }
}
