using UniSearch.Extensions;
using UniSearch.Models;
using UniSearch.Services.Base;
using UniSearch.Services.Contract;
using UniSearch.ViewModels;
using System;
using System.Linq;

namespace UniSearch.Services.Provider
{
    public class UserRegistrationService : Service<UserLogin>, IUserRegistrationService
    {
        private ApiResponse _apiResponse;
        public UserRegistrationService()
        {
            _apiResponse = new ApiResponse();
        }

        public ApiResponse AddOrUpdatUserRegistration(UserRegistrationViewModel userLoginDetail)
        {
            CustomLogger.Debug("========= START AddOrUpdatUserRegistration() ===========");
            {

                int isAdded = 0;

                if (!string.IsNullOrEmpty(userLoginDetail.LoginId))
                {
                    var data = GetFirst(x => x.LoginId == new Guid(userLoginDetail.LoginId));
                    dynamic isExist = null;
                    if (data.Email != userLoginDetail.Email)
                    {
                        isExist = GetFirst(x => x.Email == userLoginDetail.Email);  
                    }
                    if (isExist == null)
                    {
                        data.LastName = userLoginDetail.LastName;
                        data.FirstName = userLoginDetail.FirstName;
                        data.Email = userLoginDetail.Email;
                        data.Phone = userLoginDetail.Phone;
                        data.RoleId = new Guid(userLoginDetail.RoleId);
                        data.UserName = userLoginDetail.UserName;
                        
                        //data.password = Secure.EncryptData(userLoginDetail.password);

                        isAdded = Update(data);
                        if (isAdded > 0)
                        {
                            _apiResponse.statusCode = "000";
                            _apiResponse.message = "Data has been Updated Successfully";
                        }
                    }
                    else
                    {
                        _apiResponse.statusCode = "10";
                        _apiResponse.message = "Email Already Exist";
                    }


                }
                else
                {
                    var isExist = GetFirst(x => x.Email == userLoginDetail.Email);
                    if (isExist == null)
                    {
                        UserLogin userLogin = new UserLogin();
                        userLogin.isActive = true;
                        userLogin.CreatedDate = DateTime.Now;
                        userLogin.password = Secure.EncryptData(userLoginDetail.password);
                        userLogin.LastName = userLoginDetail.LastName;
                        userLogin.FirstName = userLoginDetail.FirstName;
                        userLogin.Email = userLoginDetail.Email;
                        userLogin.Phone = userLoginDetail.Phone;
                        userLogin.RoleId = new Guid(userLoginDetail.RoleId);
                        userLogin.UserName = userLoginDetail.UserName;
                        userLogin.CreatedBy = userLoginDetail.CreatedBy;

                        isAdded = Add(userLogin);
                        if (isAdded > 0)
                        {
                            _apiResponse.statusCode = "00";
                            _apiResponse.message = "Data has been Added Successfully";
                            
                        }
                    }
                    else
                    {
                        _apiResponse.statusCode = "10";
                        _apiResponse.message = "Email Already Exist";
                    }
                }

            }
            return _apiResponse;
        }


        public ApiResponse GetUserRegistrationDetail(UserRegistrationViewModel userLoginDetail)
        {
            CustomLogger.Debug("========= START GetUserRegistrationDetail() ===========");
            if (!string.IsNullOrEmpty(userLoginDetail.LoginId))
            {


                _apiResponse.data = GetList(x => x.LoginId == new Guid(userLoginDetail.LoginId), x => x.UserRole).Select(x => new { x.UserName, x.FirstName, x.LastName, x.Email, x.Phone, x.RoleId, x.UserRole.RoleName, x.CreatedBy, x.isActive }).OrderByDescending(x => x.RoleId).FirstOrDefault();

                if (_apiResponse.data != null)
                {
                    _apiResponse.statusCode = "00";
                    _apiResponse.message = "Data has been fetched successfully";
                }
            }
            else
            {
                _apiResponse.data = GetAll(x => x.UserRole).Select(x => new { x.LoginId, x.UserName, x.FirstName, x.LastName, x.Email, x.Phone, x.RoleId, x.UserRole.RoleName, x.CreatedBy, x.isActive, x.CreatedDate }).OrderByDescending(x => x.LoginId).ToList();
                if (_apiResponse.data != null)
                {
                    _apiResponse.statusCode = "00";
                    _apiResponse.message = "Data has been fetched successfully";
                }
            }
            return _apiResponse;
        }


        public ApiResponse DeleteUserDetail(ActiveViewModel usertDetail)
        {
            CustomLogger.Debug("================== API DELETEAGENTDETAIL() ===================");
            var data = GetFirst(x => x.LoginId == usertDetail.Id);
            data.isActive = usertDetail.IsActive;
            int isDeleted = Update(data);
            if (isDeleted > 0)
            {
                _apiResponse.statusCode = "00";
                if (usertDetail.IsActive)
                {
                    _apiResponse.message = "Data has been Activated Successfully";

                }
                else
                {
                    _apiResponse.message = "Data has been deleted Successfully";

                }
            }
            return _apiResponse;
        }
    }
}
