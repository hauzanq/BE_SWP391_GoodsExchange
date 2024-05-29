using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.Services;
using GoodsExchange.BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/categorys")]
    public class CategoryController : ControllerBase
    {

        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.GetById(id);
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
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
