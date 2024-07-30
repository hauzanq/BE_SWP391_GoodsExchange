using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodsExchange.API.Controllers
{
    [Route("/api/v1/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IServiceWrapper _service;
        public TransactionController(IServiceWrapper service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("transactions")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<TransactionViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTransactionsAsync([FromQuery] PagingRequestModel paging)
        {
            var result = await _service.TransactionService.GetTransactionsAsync(paging);
            return Ok(result);
        }
    }
}
