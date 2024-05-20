using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.ViewModels;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IProductService
    {
        Task<ProductViewModel> CreateProduct(CreateProductRequestModel productCreate);
        Task<ProductViewModel> UpdateProduct(UpdateProductRequestModel productUpdate);
        Task<bool> DeleteProduct(int idTmp);
        Task<List<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(int idTmp);
    }

    public class ProductService : IProductService
    {
        public Task<ProductViewModel> CreateProduct(CreateProductRequestModel productCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> UpdateProduct(UpdateProductRequestModel productUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
