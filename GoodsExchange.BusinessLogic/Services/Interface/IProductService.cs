using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.Services.Implementation;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IProductService
    {
        Task<ResponseModel<ProductViewModel>> CreateProduct(CreateProductRequestModel request);
        Task<ResponseModel<ProductViewModel>> UpdateProduct(UpdateProductRequestModel request);
        Task<ResponseModel<bool>> DeleteProduct(Guid id);
        Task<ResponseModel<PageResult<ProductViewModel>>> GetProducts(PagingRequestModel request, string? keyword, ProductsRequestModel model, bool seller = false, bool moderator = false);
        Task<ResponseModel<ProductDetailsViewModel>> GetById(Guid id);
        Task<ResponseModel<bool>> UpdateProductStatus(Guid id, bool status);
        Task<ResponseModel<bool>> ApproveProduct(Guid id);
        Task<ResponseModel<bool>> DenyProduct(Guid id);
        Task<ResponseModel<PurchaseProductViewModel>> PurchaseProduct(Guid id);
        Task<Product> GetProductAsync(Guid id);
        Task<bool> IsProductBelongToSeller(Guid productId, Guid sellerId);
    }
}
