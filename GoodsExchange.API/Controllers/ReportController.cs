using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        [Route("send-report")]
        [Authorize]
        public async Task<IActionResult> SendReport(CreateReportRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reportService.SendReport(request);
            
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);    
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        public async Task<IActionResult> GetAll([FromQuery]PagingRequestModel paging, [FromQuery] ReportsRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reportService.GetAll(paging, request);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _reportService.GetById(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("approve/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        public async Task<IActionResult> ApproveReport(Guid id)
        {
            var result = await _reportService.ApproveReport(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

    }

}
