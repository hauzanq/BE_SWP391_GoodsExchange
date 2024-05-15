using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.ViewModels;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IProductService
    {
        public ProductViewModel CreateProduct(CreateProductRequestModel productCreate);
        public ProductViewModel UpdateProduct(UpdateProductRequestModel productUpdate);
        public bool DeleteProduct(int idTmp);
        public List<ProductViewModel> GetAll();
        public ProductViewModel GetById(int idTmp);
    }

    public class ProductService : IProductService
    {

        public ProductService()
        {

        }

        public ProductViewModel CreateProduct(CreateProductRequestModel productCreate)
        {
            throw new NotImplementedException();
        }

        public ProductViewModel UpdateProduct(UpdateProductRequestModel productUpdate)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(int idTmp)
        {
            throw new NotImplementedException();
        }

        public List<ProductViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductViewModel GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

    }

}
