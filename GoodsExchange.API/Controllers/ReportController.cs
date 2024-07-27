using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;

        public ReportController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpPost]
        [Route("send-report")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseModel<ReportViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendReport(CreateReportRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceWrapper.ReportServices.SendReport(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(PageResult<ReportViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequestModel paging, [FromQuery] ReportsRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceWrapper.ReportServices.GetReports(paging, request);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(ResponseModel<ReportViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceWrapper.ReportServices.GetById(id);
            return Ok(result);
        }

        [HttpPatch]
        [Route("approve/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ApproveReport(Guid id)
        {
            var result = await _serviceWrapper.ReportServices.ApproveReport(id);
            return Ok(result);
        }

        [HttpPatch]
        [Route("deny/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DenyReport(Guid id)
        {
            var result = await _serviceWrapper.ReportServices.DenyReport(id);
            return Ok(result);
        }
    }
}
