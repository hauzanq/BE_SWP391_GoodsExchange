﻿using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using GoodsExchange.Data.Enums;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IProductService
    {
        Task<ResponseModel<ProductListViewModel>> CreateProductAsync(CreateProductRequestModel request);
        Task<ResponseModel<ProductListViewModel>> UpdateProductAsync(UpdateProductRequestModel request);
        Task<ResponseModel<bool>> DeleteProductAsync(Guid id);
        Task<ResponseModel<PageResult<ProductListViewModel>>> GetProducts(PagingRequestModel request, string? keyword, ProductsRequestModel model, string role);
        Task<ResponseModel<ProductDetailViewModel>> GetProductDetailAsync(Guid id);
        Task<ResponseModel<PageResult<UserProductListViewModel>>> GetProductsForUserAsync(PagingRequestModel request);
        Task<ResponseModel<UserProductDetailViewModel>> GetUserProductDetailAsync(Guid id);
        Task<ResponseModel<PageResult<UserProductListViewModel>>> GetProductsForUserProfileAsync(PagingRequestModel request, Guid id);
        Task<ResponseModel<bool>> UpdateProductStatusAsync(Guid id, ProductStatus status);
        Task<Product> GetProductAsync(Guid id);
        Task<bool> IsProductBelongToSellerAsync(Guid productId, Guid sellerId);
    }
}
