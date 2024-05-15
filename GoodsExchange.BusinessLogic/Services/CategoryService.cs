using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.ViewModels;
namespace GoodsExchange.BusinessLogic.Services
{

    public interface ICategoryService
    {
        public CategoryViewModel CreateCategory(CreateCategoryRequestModel categoryCreate);
        public CategoryViewModel UpdateCategory(UpdateCategoryRequestModel categoryUpdate);
        public bool DeleteCategory(int idTmp);
        public List<CategoryViewModel> GetAll();
        public CategoryViewModel GetById(int idTmp);
    }

    public class CategoryService : ICategoryService
    {

        public CategoryService()
        {
                
        }
        public CategoryViewModel CreateCategory(CreateCategoryRequestModel categoryCreate)
        {
            throw new NotImplementedException();
        }

        public CategoryViewModel UpdateCategory(UpdateCategoryRequestModel categoryUpdate)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategory(int idTmp)
        {
            throw new NotImplementedException();
        }

        public List<CategoryViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public CategoryViewModel GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

    }

}
