using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/products")]
    public class ProductController : ControllerBase
    {

        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        [Route("create")]
        [Authorize(Roles = SystemConstant.Roles.Seller)]
        public async Task<IActionResult> CreateProduct([FromQuery] CreateProductRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.CreateProduct(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _productService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequestModel request, [FromQuery] SearchRequestModel search, [FromQuery] GetAllProductRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetProductsAsync(request, search, model);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = SystemConstant.Roles.Seller)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = SystemConstant.Roles.Seller)]
        public async Task<IActionResult> UpdateProduct([FromQuery] UpdateProductRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateProduct(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch]
        [Route("status/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Seller)]
        public async Task<IActionResult> UpdateProductStatus(Guid id, bool status)
        {
            var result = await _productService.UpdateProductStatus(id, status);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Route("approve/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        public async Task<IActionResult> ApproveProduct(Guid id)
        {
            var result = await _productService.ApproveProduct(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
