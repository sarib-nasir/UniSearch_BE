using UniSearch.Extensions;
using UniSearch.Models;
using UniSearch.Services.Base;
using UniSearch.Services.Contract;
using UniSearch.ViewModels;
using System;
using System.Linq;

namespace UniSearch.Services.Provider
{
    public class ProgramsService : Service<PROGRAMS>, IProgramsService
    {
        private ApiResponse _apiResponse;

        public ProgramsService()
        {
            _apiResponse = new ApiResponse();
        }

        public ApiResponse GetProgramDetail(ProgramsViewModel model)
        {

            CustomLogger.Debug("========= START GetProgramsList() ===========");
            if (model.PROGRAM_ID != Guid.Empty)
            {
                _apiResponse.data = GetFirst(x => x.IS_ACTIVE == true && x.PROGRAM_ID == model.PROGRAM_ID);
                if (_apiResponse.data != null)
                {
                    _apiResponse.statusCode = "00";
                    _apiResponse.message = "Data has been fetched successfully";
                }
            }
            else
            {
                _apiResponse.data = GetList(x => x.IS_ACTIVE == true, x => x.UNIVERSITIES, x => x.LANGUAGES).ToList();
                if (_apiResponse.data != null)
                {
                    _apiResponse.statusCode = "00";
                    _apiResponse.message = "Data has been fetched successfully";
                }
            }
            return _apiResponse;
        }
    }
}
