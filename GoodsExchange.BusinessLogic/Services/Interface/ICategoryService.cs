using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.ViewModels.Category;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface ICategoryService
    {
        Task<EntityResponse<CategoryViewModel>> CreateCategory(CreateCategoryRequestModel categoryCreate);
        Task<EntityResponse<CategoryViewModel>> UpdateCategory(UpdateCategoryRequestModel categoryUpdate);
        Task<EntityResponse<bool>> DeleteCategory(Guid id);
        Task<EntityResponse<List<CategoryViewModel>>> GetCategories();
        Task<EntityResponse<CategoriesDetailViewModel>> GetById(Guid idTmp);
        Task<Category> GetCategoryAsync(Guid id);
    }
}
