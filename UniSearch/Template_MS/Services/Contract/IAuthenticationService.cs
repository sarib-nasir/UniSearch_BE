using UniSearch.Extensions;
using UniSearch.Models;
using UniSearch.Services.Base;
using UniSearch.ViewModels;
using System.Threading.Tasks;

namespace UniSearch.Services.Contract
{
    public interface IAuthenticationService : IService<UserLogin>
    {
        Task<ApiResponse> Authenticate(LoginViewModel obj);

    }
}
