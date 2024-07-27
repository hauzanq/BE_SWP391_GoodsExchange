using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.ExchangeRequest;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.ExchangeRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodsExchange.API.Controllers
{
    [Route("api/v1/exchanges")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IServiceWrapper _service;
        public ExchangeController(IServiceWrapper service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("send-request")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<ExchangeRequestViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendExchangeRequestAsync([FromBody] CreateExchangeRequestModel request)
        {
            var result = await _service.ExchangeRequestService.SendExchangeRequestAsync(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("confirm-request")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<ExchangeRequestViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ConfirmExchangeAsync(Guid requestid)
        {
            var result = await _service.ExchangeRequestService.ConfirmExchangeAsync(requestid);
            return Ok(result);
        }

        [HttpPost]
        [Route("deny-request")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<ExchangeRequestViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DenyExchangeAsync(Guid requestid)
        {
            var result = await _service.ExchangeRequestService.DenyExchangeAsync(requestid);
            return Ok(result);
        }

        [HttpGet]
        [Route("send-request-list")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<ExchangeRequestViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSentExchangeRequestsAsync()
        {
            var result = await _service.ExchangeRequestService.GetSentExchangeRequestsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("receive-request-list")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<ExchangeRequestViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetReceivedExchangeRequestsAsync()
        {
            var result = await _service.ExchangeRequestService.GetReceivedExchangeRequestsAsync();
            return Ok(result);
        }
    }
}
