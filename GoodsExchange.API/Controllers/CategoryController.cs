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
        public ActionResult<CategoryViewModel> CreateCategory(CreateCategoryRequestModel categoryCreate)
        {
            var categoryCreated = _categoryService.CreateCategory(categoryCreate);

            if (categoryCreated == null)
            {
                return NotFound("");
            }
            return categoryCreated;
        }


        [HttpGet]
        public ActionResult<List<CategoryViewModel>> GetAll()
        {
            var categoryList = _categoryService.GetAll();

            if (categoryList == null)
            {
                return NotFound("");
            }
            return categoryList;
        }


        [HttpGet("idTmp")]
        public ActionResult<CategoryViewModel> GetById(int idTmp)
        {
            var categoryDetail = _categoryService.GetById(idTmp);

            if (categoryDetail == null)
            {
                return NotFound("");
            }
            return categoryDetail;
        }


        [HttpDelete]
        public ActionResult<bool> DeleteCategory(int idTmp)
        {
            var check = _categoryService.DeleteCategory(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }


        [HttpPut]
        public ActionResult<CategoryViewModel> UpdateCategory(UpdateCategoryRequestModel categoryCreate)
        {
            var categoryUpdated = _categoryService.UpdateCategory(categoryCreate);

            if (categoryUpdated == null)
            {
                return NotFound("");
            }
            return categoryUpdated;
        }
    }

}
