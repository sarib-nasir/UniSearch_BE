using UniSearch.Extensions;
using UniSearch.Models;
using UniSearch.Services.Base;
using UniSearch.Services.Contract;
using UniSearch.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using LinqKit;

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
            try {
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
                    var expr = PredicateBuilder.New<PROGRAMS>(true);
                    bool isFilter = false;

                    if (!string.IsNullOrEmpty(model.PROGRAM_NAME))
                    {
                        expr.And(x => x.PROGRAM_TAGS != null &&
              x.PROGRAM_TAGS.ToLower().Contains(model.PROGRAM_NAME.ToLower()));

                        isFilter = true;
                    }
                    if (!string.IsNullOrEmpty(model.IELTS_SCORE))
                    {
                        expr.And(x => float.Parse(x.IELTS_SCORE) <= float.Parse(model.IELTS_SCORE));
                        isFilter = true;
                    }
                    if (isFilter)
                    {
                        _apiResponse.data = GetList(expr, x => x.UNIVERSITIES, x => x.LANGUAGES).ToList();
                    }

                    //_apiResponse.data = GetList(x => x.IS_ACTIVE == true, x => x.UNIVERSITIES, x => x.LANGUAGES).ToList();
                    if (_apiResponse.data != null)
                    {
                        _apiResponse.statusCode = "00";
                        _apiResponse.message = "Data has been fetched successfully";
                    }
                    // Define which columns you want
                    //var selectedColumns = new List<string> {"PROGRAM_NAME", };

                    //var data = GetList(x => x.IS_ACTIVE == true, x => x.UNIVERSITIES, x => x.LANGUAGES).ToList();

                    //// Extract only the selected column values (without keys)
                    ////var filteredData = data.Select(item =>
                    ////    selectedColumns
                    ////        .Select(col => item.GetType().GetProperty(col)?.GetValue(item)) // ✅ Only values, no keys
                    ////        .ToList()
                    ////).ToList();

                    //// Final response format
                    //var response = new
                    //{
                    //    Columns = selectedColumns,  // ✅ Column headers
                    //    Data = data         // ✅ Only values (not key-value pairs)
                    //};

                    //_apiResponse.data = response;
                    //if (_apiResponse.data != null)
                    //{
                    //    _apiResponse.statusCode = "00";
                    //    _apiResponse.message = "Data has been fetched successfully";
                    //}

                }
            }
            catch(Exception ex)
            {

            }
            return _apiResponse;
        }
    }
}
