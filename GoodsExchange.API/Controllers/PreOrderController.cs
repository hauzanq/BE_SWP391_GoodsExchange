//using GoodsExchange.API.Middlewares;
//using GoodsExchange.BusinessLogic.Common;
//using GoodsExchange.BusinessLogic.Constants;
//using GoodsExchange.BusinessLogic.RequestModels.PreOrder;
//using GoodsExchange.BusinessLogic.Services.Interface;
//using GoodsExchange.BusinessLogic.ViewModels.PreOrder;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace GoodsExchange.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PreOrderController : ControllerBase
//    {
//        private readonly IPreOrderService _preOrderService;
//        public PreOrderController(IPreOrderService preOrderService)
//        {
//            _preOrderService = preOrderService;
//        }

//        [HttpPost("create")]
//        [Authorize(Roles = SystemConstant.Roles.Customer)]
//        [ProducesResponseType(typeof(ResponseModel<PreOrderViewModel>), (int)HttpStatusCode.OK)]
//        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
//        public async Task<IActionResult> CreatePreOrderAsync(CreatePreOrderRequestModel request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var result = await _preOrderService.CreatePreOrderAsync(request);
//            return Ok(result);
//        }

//        [HttpPost("confirm")]
//        [Authorize(Roles = SystemConstant.Roles.Customer)]
//        [ProducesResponseType(typeof(ResponseModel<ConfirmPreOrderViewModel>), (int)HttpStatusCode.OK)]
//        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
//        public async Task<IActionResult> ConfirmPreOrderAsync(ConfirmPreOrderRequestModel request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var result = await _preOrderService.ConfirmPreOrderAsync(request);
//            return Ok(result);
//        }

//        [HttpGet("preorders")]
//        [Authorize(Roles = SystemConstant.Roles.Customer)]
//        [ProducesResponseType(typeof(ResponseModel<PreOrderViewModel>), (int)HttpStatusCode.OK)]
//        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
//        public async Task<IActionResult> GetPreOrdersForProductAsync(Guid productid)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var result = await _preOrderService.GetPreOrdersForProductAsync(productid);
//            return Ok(result);
//        }
//    }
//}
