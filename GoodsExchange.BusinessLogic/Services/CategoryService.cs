using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.ViewModels;
namespace GoodsExchange.BusinessLogic.Services
{

    public interface ICategoryService
    {
        Task<CategoryViewModel> CreateCategory(CreateCategoryRequestModel categoryCreate);
        Task<CategoryViewModel> UpdateCategory(UpdateCategoryRequestModel categoryUpdate);
        Task<bool> DeleteCategory(int idTmp);
        Task<List<CategoryViewModel>> GetAll();
        Task<CategoryViewModel> GetById(int idTmp);
    }

    public class CategoryService : ICategoryService
    {
        public Task<CategoryViewModel> CreateCategory(CreateCategoryRequestModel categoryCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryViewModel> GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryViewModel> UpdateCategory(UpdateCategoryRequestModel categoryUpdate)
        {
            throw new NotImplementedException();
        }
    }

}
