using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/categories")]

    public class CategoryController : ControllerBase
    {

        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]

        public async Task<ActionResult<CategoryViewModel>> CreateCategory(CreateCategoryRequestModel categoryCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.CreateCategory(categoryCreate);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<CategoryViewModel>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.GetAll();
            return Ok(result);
        }


        [HttpGet("id")]
        public async Task<ActionResult<CategoryViewModel>> GetById(Guid id)
        {
            var result = await _categoryService.GetById(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpDelete]
        [Route("id")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]

        public async Task<ActionResult<bool>> DeleteCategory(Guid id)
        {
            var result = await _categoryService.DeleteCategory(id);  
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPut]
        public async Task<ActionResult<CategoryViewModel>> UpdateCategory(UpdateCategoryRequestModel categoryCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.UpdateCategory(categoryCreate);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }

}
