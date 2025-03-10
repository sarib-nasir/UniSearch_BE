using UniSearch.Extensions;
using UniSearch.Models;
using UniSearch.Services.Base;
using UniSearch.Services.Contract;
using UniSearch.ViewModels;
using System;
using System.Linq;

namespace UniSearch.Services.Provider
{
    public class RoleService : Service<UserRole>, IRoleService
    {
        private ApiResponse _apiResponse;

        public RoleService()
        {
            _apiResponse = new ApiResponse();
        }

        public ApiResponse GetRoleDetail(UserRoleViewModel roleDetail)
        {

            CustomLogger.Debug("========= START GetRoleDetail() ===========");
            if (roleDetail.RoleId != Guid.Empty)
            {
                _apiResponse.data = GetFirst(x => x.IsActive == true && x.RoleId == roleDetail.RoleId);
                if (_apiResponse.data != null)
                {
                    _apiResponse.statusCode = "00";
                    _apiResponse.message = "Data has been fetched successfully";
                }
            }
            else
            {
                _apiResponse.data = GetList(x => x.IsActive == true).OrderByDescending(x => x.RoleId);
                if (_apiResponse.data != null)
                {
                    _apiResponse.statusCode = "00";
                    _apiResponse.message = "Data has been fetched successfully";
                }
            }
            return _apiResponse;
        }

        public ApiResponse AddOrUpdateRole(UserRoleViewModel roleDetail)
        {
            try
            {


                CustomLogger.Debug("========= START AddOrUpdateRole() ===========");
                if (roleDetail.RoleId != Guid.Empty)
                {
                    var data = GetFirst(x => x.RoleId == roleDetail.RoleId);
                    data.RoleName = roleDetail.RoleName;
                    var isUpdate = Update(data);
                    if (isUpdate > 0)
                    {
                        _apiResponse.statusCode = "000";
                        _apiResponse.message = "Data has been Updated Successfully";
                    }
                }
                else
                {
                    UserRole userRole = new UserRole();
                    userRole.RoleName = roleDetail.RoleName;
                    userRole.IsActive = true;
                    var isAdded = Add(userRole);
                    if (isAdded > 0)
                    {
                        _apiResponse.statusCode = "00";
                        _apiResponse.message = "Data has been Added Successfully";
                    }
                }

            }
            catch (Exception ex)
            {

                CustomLogger.WriteErrorLogToFile(ex);
            }

            return _apiResponse;
        }

        public ApiResponse DeleteRoleDetail(UserRoleViewModel roleDetail)
        {
            try
            {

                CustomLogger.Debug("========= START DeleteRoleDetail() ===========");
                var data = GetFirst(x => x.RoleId == roleDetail.RoleId);
                int isDeleted = Update(data);
                if (isDeleted > 0)
                {
                    _apiResponse.statusCode = "00";
                    _apiResponse.message = "Data has been deleted Successfully";
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
