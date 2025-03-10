using UniSearch.Extensions;
using UniSearch.Models;
using UniSearch.Services.Base;
using UniSearch.ViewModels;

namespace UniSearch.Services.Contract
{
    public interface IRoleService : IService<UserRole>
    {
        ApiResponse GetRoleDetail(UserRoleViewModel model);
        ApiResponse AddOrUpdateRole(UserRoleViewModel model);
        ApiResponse DeleteRoleDetail(UserRoleViewModel model);
    }
}
