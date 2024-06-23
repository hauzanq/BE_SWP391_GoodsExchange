using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = SystemConstant.Roles.Customer)]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestModel request)
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

        [HttpPost]
        [Route("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductForBuyer([FromQuery] PagingRequestModel request, [FromQuery] SearchRequestModel search, [FromQuery] GetAllProductRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAll(request, search, model);
            return Ok(result);
        }


        [HttpPost]
        [Route("seller/all")]
        [Authorize(Roles = SystemConstant.Roles.Customer)]
        public async Task<IActionResult> GetProductsBySeller([FromQuery] PagingRequestModel request, [FromQuery] SearchRequestModel search, [FromQuery] GetAllProductRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAll(request, search, model, true, false);
            return Ok(result);
        }

        [HttpPost]
        [Route("moderator/all")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        public async Task<IActionResult> GetProductForModerator([FromQuery] PagingRequestModel request, [FromQuery] SearchRequestModel search, [FromQuery] GetAllProductRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetAll(request, search, model, false, true);
            return Ok(result);
        }


        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductDetails(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.GetById(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = SystemConstant.Roles.Customer)]
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
        [Authorize(Roles = SystemConstant.Roles.Customer)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequestModel request)
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
        [Authorize(Roles = SystemConstant.Roles.Customer)]
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

        [HttpPatch]
        [Route("deny/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        public async Task<IActionResult> DenyProduct(Guid id)
        {
            var result = await _productService.DenyProduct(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
