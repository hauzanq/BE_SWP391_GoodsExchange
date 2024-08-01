using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using GoodsExchange.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;

        public ProductController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpPost("create")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<ProductListViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.ProductServices.CreateProductAsync(request);
            return Ok(result);
        }

        [HttpPost("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PageResult<ProductListViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProducts([FromQuery] PagingRequestModel request, [FromQuery] string? keyword, [FromQuery] ProductsRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.ProductServices.GetProducts(request, keyword, model, SystemConstant.Roles.Guest);
            return Ok(result);
        }


        [HttpPost("user")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(PageResult<ProductListViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductsForUserAsync([FromQuery] PagingRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.ProductServices.GetProductsForUserAsync(request);
            return Ok(result);
        }

        [HttpPost("user/{id}")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(PageResult<ProductListViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductsForUserProfileAsync([FromQuery] PagingRequestModel request, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.ProductServices.GetProductsForUserProfileAsync(request, id);
            return Ok(result);
        }

        [HttpPost("moderator/all")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(PageResult<ProductListViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductsForModerator([FromQuery] PagingRequestModel request, [FromQuery] string? keyword, [FromQuery] ProductsRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.ProductServices.GetProducts(request, keyword, model, SystemConstant.Roles.Moderator);
            return Ok(result);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<ProductListViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductDetails(Guid id)
        {
            var result = await _serviceWrapper.ProductServices.GetProductDetailAsync(id);
            return Ok(result);
        }

        [HttpPut("update")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<ProductListViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.ProductServices.UpdateProductAsync(request);
            return Ok(result);
        }

        [HttpPatch("status/{id}")]
        [Authorize(Roles = SystemConstant.Roles.User + "," + SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProductStatus(Guid id, ProductStatus status)
        {
            var result = await _serviceWrapper.ProductServices.UpdateProductStatusAsync(id, status);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            var result = await _serviceWrapper.ProductServices.DeleteProductAsync(id);
            return Ok(result);
        }
    }
}
