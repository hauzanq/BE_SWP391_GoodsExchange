using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IProductService
    {
        Task<EntityResponse<ProductViewModel>> CreateProduct(CreateProductRequestModel request);
        Task<EntityResponse<ProductViewModel>> UpdateProduct(UpdateProductRequestModel request);
        Task<EntityResponse<bool>> DeleteProduct(Guid id);
        Task<PageResult<ProductViewModel>> GetProducts(PagingRequestModel request, string? keyword, ProductsRequestModel model, bool seller = false, bool moderator = false);
        Task<EntityResponse<ProductDetailsViewModel>> GetById(Guid id);
        Task<EntityResponse<bool>> UpdateProductStatus(Guid id, bool status);
        Task<EntityResponse<bool>> ApproveProduct(Guid id);
        Task<EntityResponse<bool>> DenyProduct(Guid id);

        Task<Product> GetProductAsync(Guid id);
        Task<bool> IsProductBelongToSeller(Guid productId, Guid sellerId);
    }
}
