using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.Domain;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/pollsters/grades")]
    [ApiController]
    public class PollsterGradeApiController : BaseApiController
    {
        private IPollsterGradeService _gradeService = null;

        public PollsterGradeApiController(IPollsterGradeService gradeService
            , ILogger<PollsterGradeApiController> logger)
            : base(logger)
        {
            _gradeService = gradeService;
        }

        [AllowAnonymous]
        [HttpGet("{electionId:int}")]
        public ActionResult<ItemsResponse<PollsterGrade>> GetByElectionId(int electionId)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                List<PollsterGrade> grade = _gradeService.GetByElectionId(electionId);

                if (grade == null)
                {
                    code = 404;
                    response = new ErrorResponse("Records not found");
                }
                else
                {
                    response = new ItemsResponse<PollsterGrade> { Items = grade };
                }
            }

            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
    }
}
