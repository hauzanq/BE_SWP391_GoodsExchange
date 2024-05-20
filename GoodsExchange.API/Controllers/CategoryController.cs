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
            var categoryCreated = await _categoryService.CreateCategory(categoryCreate);

            if (categoryCreated == null)
            {
                return NotFound("");
            }
            return categoryCreated;
        }


        [HttpGet]
        public async Task<ActionResult<List<CategoryViewModel>>> GetAll()
        {
            var categoryList = await _categoryService.GetAll();

            if (categoryList == null)
            {
                return NotFound("");
            }
            return categoryList;
        }


        [HttpGet("idTmp")]
        public async Task<ActionResult<CategoryViewModel>> GetById(int idTmp)
        {
            var categoryDetail = await _categoryService.GetById(idTmp);

            if (categoryDetail == null)
            {
                return NotFound("");
            }
            return categoryDetail;
        }


        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCategory(int idTmp)
        {
            var check = await _categoryService.DeleteCategory(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }


        [HttpPut]
        public async Task<ActionResult<CategoryViewModel>> UpdateCategory(UpdateCategoryRequestModel categoryCreate)
        {
            var categoryUpdated = await _categoryService.UpdateCategory(categoryCreate);

            if (categoryUpdated == null)
            {
                return NotFound("");
            }
            return categoryUpdated;
        }
    }

}
