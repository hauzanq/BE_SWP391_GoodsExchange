using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IProductService
    {
        Task<ApiResult<ProductViewModel>> CreateProduct(CreateProductRequestModel request);
        Task<ApiResult<ProductViewModel>> UpdateProduct(UpdateProductRequestModel request);
        Task<ApiResult<bool>> DeleteProduct(Guid id);
        Task<PageResult<ProductViewModel>> GetProductsAsync(PagingRequestModel request, SearchRequestModel search, GetAllProductRequestModel model);
        Task<ApiResult<ProductViewModel>> GetById(Guid id);
        Task<ApiResult<ProductDetailsViewModel>> GetProductDetails(Guid id);
        Task<ApiResult<bool>> UpdateProductStatus(Guid id, bool status);
        Task<ApiResult<bool>> ApproveProduct(Guid id);
    }

    public class ProductService : IProductService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IRatingService _ratingService;
        public ProductService(GoodsExchangeDbContext context, IRatingService ratingService)
        {
            _context = context;
            _ratingService = ratingService;
        }

        public async Task<ApiResult<bool>> ApproveProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Product does not exists.");
            }
            product.IsApproved = true;
            product.ApprovedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<ProductViewModel>> CreateProduct(CreateProductRequestModel request)
        {
            var product = new Product()
            {
                ProductName = request.ProductName,
                Description = request.Description,
                ProductImageUrl = request.ProductImageUrl,
                Price = request.Price,
                CategoryId = request.CategoryId,
                UploadDate = DateTime.Now,
                Status = false
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var result = new ProductViewModel()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                ProductImageUrl = product.ProductImageUrl,
                Price = product.Price,
                CategoryName = product.Category.CategoryName,
                UploadDate = product.UploadDate,
                Status = product.Status
            };

            return new ApiSuccessResult<ProductViewModel>(result);
        }

        public async Task<ApiResult<bool>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Product does not exists.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }

        public async Task<PageResult<ProductViewModel>> GetProductsAsync(PagingRequestModel paging, SearchRequestModel search, GetAllProductRequestModel model)
        {
            var query = _context.Products.Where(p => p.UserUpload.Status == true).AsQueryable();

            #region Searching
            if (!string.IsNullOrEmpty(search.KeyWords))
            {
                query = query.Where(p => p.ProductName.Contains(search.KeyWords));
            }
            #endregion

            #region Filtering

            if (!string.IsNullOrEmpty(model.ProductName))
            {
                query = query.Where(p => p.ProductName.Contains(model.ProductName));
            }
            if (model.MinPrice != null)
            {
                query = query.Where(p => p.Price >= model.MinPrice);
            }
            if (model.MaxPrice != null)
            {
                query = query.Where(p => p.Price <= model.MaxPrice);
            }
            if (model.Status != null)
            {
                query = query.Where(p => p.Status == model.Status);
            }
            if (model.StartUploadDate != null)
            {
                query = query.Where(p => p.UploadDate >= model.StartUploadDate);
            }
            if (model.EndUploadDate != null)
            {
                query = query.Where(p => p.UploadDate <= model.EndUploadDate);
            }
            if (model.IsApproved != null)
            {
                query = query.Where(p => p.IsApproved == model.IsApproved);
            }
            if (model.StartApprovedDate != null)
            {
                query = query.Where(p => p.ApprovedDate >= model.StartApprovedDate);
            }
            if (model.EndApprovedDate != null)
            {
                query = query.Where(p => p.ApprovedDate <= model.EndApprovedDate);
            }

            if (!string.IsNullOrEmpty(model.CategoryName))
            {
                query = query.Where(p => p.Category.CategoryName.Contains(model.CategoryName));
            }

            #endregion

            #region Sorting
            if (!string.IsNullOrEmpty(model.ProductName) && model.ProductName.ToLower() == "asc")
            {
                query = query.OrderBy(p => p.ProductName);
            }
            else if (!string.IsNullOrEmpty(model.ProductName) && model.ProductName.ToLower() == "desc")
            {
                query = query.OrderByDescending(p => p.ProductName);
            }


            if (model.MinPrice.HasValue && model.MaxPrice.HasValue)
            {
                if (model.MinPrice.Value <= model.MaxPrice.Value)
                {
                    query = query.OrderBy(p => p.Price);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Price);
                }
            }

            #endregion

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            var data = await query.Skip((paging.PageIndex - 1) * paging.PageSize)
                                .Take(paging.PageSize)
                                .Select(p => new ProductViewModel()
                                {
                                    ProductId = p.ProductId,
                                    ProductName = p.ProductName,
                                    Description = p.Description,
                                    ProductImageUrl = p.ProductImageUrl,
                                    Status = p.Status,
                                    Price = p.Price,
                                    CategoryName = p.Category.CategoryName,
                                    ApprovedDate = p.ApprovedDate,
                                    UploadDate = p.UploadDate,
                                })
                                .ToListAsync();

            var result = new PageResult<ProductViewModel>()
            {
                Items = data,
                TotalPage = totalPages,
                CurrentPage = paging.PageIndex
            };
            return result;
        }

        public async Task<ApiResult<ProductViewModel>> GetById(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<ProductViewModel>("Product does not exists.");
            }

            var result = new ProductViewModel()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                ProductImageUrl = product.ProductImageUrl,
                Price = product.Price,
                CategoryName = product.Category.CategoryName,
                UploadDate = product.UploadDate,
                Status = product.Status
            };

            return new ApiSuccessResult<ProductViewModel>(result);
        }

        public async Task<ApiResult<ProductViewModel>> UpdateProduct(UpdateProductRequestModel request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return new ApiErrorResult<ProductViewModel>("Product does not exists.");
            }

            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.ProductImageUrl = request.ProductImageUrl;
            product.Price = request.Price.Value;
            product.Status = request.Status.Value;
            product.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync();
            var result = new ProductViewModel()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                ProductImageUrl = product.ProductImageUrl,
                Price = product.Price,
                CategoryName = product.Category.CategoryName,
                UploadDate = product.UploadDate,
                Status = product.Status
            };

            return new ApiSuccessResult<ProductViewModel>(result);
        }

        public async Task<ApiResult<bool>> UpdateProductStatus(Guid id, bool status)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Product does not exists.");
            }

            product.Status = status;
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<ProductDetailsViewModel>> GetProductDetails(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<ProductDetailsViewModel>("Product does not exists.");
            }

            var result = new ProductDetailsViewModel()
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ProductImageUrl = product.ProductImageUrl,
                ApprovedDate = product.ApprovedDate,

                UserUpload = product.UserUpload.UserName,
                UserImageUrl = product.UserUpload.UserImageUrl,
                NumberOfRatings = _ratingService.CountNumberRatingOfUser(product.UserUpload.UserId).Result,
                AverageNumberStars = _ratingService.CountAverageNumberStarsOfUser(product.UserUpload.UserId).Result,
                UserPhoneNumber = product.UserUpload.PhoneNumber
            };

            return new ApiSuccessResult<ProductDetailsViewModel>(result);
        }
    }
}
