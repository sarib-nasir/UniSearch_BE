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
    //[Authorize]
    public class ProgramsController : ControllerBase
    {
        private ApiResponse _apiResponse;

        protected readonly IProgramsService _programsService;
        public ProgramsController(IProgramsService programsService)
        {
            _programsService = programsService;
        }

        [HttpPost]
        [Route("GetProgramsDetail")]
        public ApiResponse GetProgramsDetail([FromBody] ProgramsViewModel programsDetail)
        {
            CustomLogger.Debug("Programs Controller");

            try
            {
                _apiResponse = _programsService.GetProgramDetail(programsDetail);
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex.ToString());
            }

            return _apiResponse;
        }
    }
}
