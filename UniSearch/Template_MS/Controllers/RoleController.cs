using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniSearch.Extensions;
using UniSearch.Services.Contract;
using UniSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private ApiResponse _apiResponse;

        protected readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Route("GetRoleDetail")]
        public ApiResponse GetRoleDetail([FromBody] UserRoleViewModel roleDetail)
        {
            CustomLogger.Debug("Role Controller");

            try
            {
                _apiResponse = _roleService.GetRoleDetail(roleDetail);
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex.ToString());
            }

            return _apiResponse;
        }

        [HttpPost]
        [Route("AddOrUpdateRole")]
        public ApiResponse AddOrUpdateRole([FromBody] UserRoleViewModel roleDetail)
        {
            try
            {
                _apiResponse = _roleService.AddOrUpdateRole(roleDetail);
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex.ToString());
            }

            return _apiResponse;
        }

        [HttpPost]
        [Route("DeleteRoleDetail")]
        public ApiResponse DeleteRoleDetail([FromBody] UserRoleViewModel roleDetail)
        {
            try
            {
                _apiResponse = _roleService.DeleteRoleDetail(roleDetail);
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex.ToString());
            }

            return _apiResponse;
        }
    }
}
