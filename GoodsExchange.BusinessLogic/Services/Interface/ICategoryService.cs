using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.ViewModels.Category;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface ICategoryService
    {
        Task<ApiResult<CategoryViewModel>> CreateCategory(CreateCategoryRequestModel categoryCreate);
        Task<ApiResult<CategoryViewModel>> UpdateCategory(UpdateCategoryRequestModel categoryUpdate);
        Task<ApiResult<bool>> DeleteCategory(Guid id);
        Task<ApiResult<List<CategoryViewModel>>> GetAll();
        Task<ApiResult<CategoriesDetailViewModel>> GetById(Guid idTmp);
    }
}
