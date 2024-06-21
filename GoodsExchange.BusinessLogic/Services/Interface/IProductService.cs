using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IProductService
    {
        Task<ApiResult<ProductViewModel>> CreateProduct(CreateProductRequestModel request);
        Task<ApiResult<ProductViewModel>> UpdateProduct(UpdateProductRequestModel request);
        Task<ApiResult<bool>> DeleteProduct(Guid id);
        Task<PageResult<ProductViewModel>> GetAll(PagingRequestModel request, SearchRequestModel search, GetAllProductRequestModel model, bool seller = false, bool moderator = false);
        Task<ApiResult<ProductDetailsViewModel>> GetById(Guid id);
        Task<ApiResult<bool>> UpdateProductStatus(Guid id, bool status);
        Task<ApiResult<bool>> ReviewProduct(Guid id, bool status);
        Task<Product> GetProductAsync(Guid id);
        Task<bool> IsProductBelongToSeller(Guid productId,Guid sellerId);
    }
}
