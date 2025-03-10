using UniSearch.Extensions;
using UniSearch.Models;
using UniSearch.Services.Base;
using UniSearch.ViewModels;

namespace UniSearch.Services.Contract
{
    public interface IUserRegistrationService : IService<UserLogin>
    {

        ApiResponse AddOrUpdatUserRegistration(UserRegistrationViewModel userRegistrationViewModel);
        ApiResponse GetUserRegistrationDetail(UserRegistrationViewModel model);
        ApiResponse DeleteUserDetail(ActiveViewModel model);


    }
}
