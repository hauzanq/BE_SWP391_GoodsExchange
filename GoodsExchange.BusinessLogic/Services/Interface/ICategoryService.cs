using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.ViewModels.Category;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface ICategoryService
    {
        Task<ResponseModel<CategoryViewModel>> CreateCategory(CreateCategoryRequestModel categoryCreate);
        Task<ResponseModel<CategoryViewModel>> UpdateCategory(UpdateCategoryRequestModel categoryUpdate);
        Task<ResponseModel<bool>> DeleteCategory(Guid id);
        Task<ResponseModel<List<CategoryViewModel>>> GetCategories();
        Task<Category> GetCategoryAsync(Guid id);
    }
}
