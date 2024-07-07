using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        [Authorize(Roles = SystemConstant.Roles.Customer)]
        [ProducesResponseType(typeof(ResponseModel<ProductViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.CreateProduct(request);
            return Ok(result);
        }

        [HttpPost("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PageResult<ProductViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductForBuyer([FromQuery] PagingRequestModel request, [FromQuery] string? keyword, [FromQuery] ProductsRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetProducts(request, keyword, model);
            return Ok(result);
        }


        [HttpPost("seller/all")]
        [Authorize(Roles = SystemConstant.Roles.Customer + "," + SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(PageResult<ProductViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductsBySeller([FromQuery] PagingRequestModel request, [FromQuery] string? keyword, [FromQuery] ProductsRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetProducts(request, keyword, model, true, false);
            return Ok(result);
        }

        [HttpPost("moderator/all")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(PageResult<ProductViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductForModerator([FromQuery] PagingRequestModel request, [FromQuery] string? keyword, [FromQuery] ProductsRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetProducts(request, keyword, model, false, true);
            return Ok(result);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<ProductViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductDetails(Guid id)
        {
            var result = await _productService.GetById(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = SystemConstant.Roles.Customer)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }

        [HttpPut("update")]
        [Authorize(Roles = SystemConstant.Roles.Customer)]
        [ProducesResponseType(typeof(ResponseModel<ProductViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateProduct(request);
            return Ok(result);
        }

        [HttpPatch("status/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Customer)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProductStatus(Guid id, bool status)
        {
            var result = await _productService.UpdateProductStatus(id, status);
            return Ok(result);
        }

        [HttpPatch("approve/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ApproveProduct(Guid id)
        {
            var result = await _productService.ApproveProduct(id);
            return Ok(result);
        }

        [HttpPatch("deny/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DenyProduct(Guid id)
        {
            var result = await _productService.DenyProduct(id);
            return Ok(result);
        }
    }
}
