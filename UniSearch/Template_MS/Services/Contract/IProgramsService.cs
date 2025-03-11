using UniSearch.Extensions;
using UniSearch.Models;
using UniSearch.Services.Base;
using UniSearch.ViewModels;

namespace UniSearch.Services.Contract
{
    public interface IProgramsService : IService<PROGRAMS>
    {
        ApiResponse GetProgramDetail(ProgramsViewModel model);
    }
}
