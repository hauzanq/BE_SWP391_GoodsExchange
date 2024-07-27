using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/categories")]

    public class CategoryController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;

        public CategoryController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpPost]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(ResponseModel<CategoryViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CategoryViewModel>> CreateCategory(CreateCategoryRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.CategoryServices.CreateCategory(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<PageResult<CategoryViewModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CategoryViewModel>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.CategoryServices.GetCategories();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseModel<CategoryViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CategoryViewModel>> GetById(Guid id)
        {
            var result = await _serviceWrapper.CategoryServices.GetById(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> DeleteCategory(Guid id)
        {
            var result = await _serviceWrapper.CategoryServices.DeleteCategory(id);
            return Ok(result);
        }


        [HttpPut]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(ResponseModel<CategoryViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CategoryViewModel>> UpdateCategory(UpdateCategoryRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.CategoryServices.UpdateCategory(request);
            return Ok(result);
        }
    }

}
