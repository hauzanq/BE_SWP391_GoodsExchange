using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services
{
    public interface IProductService
    {
        Task<ApiResult<ProductViewModel>> CreateProduct(CreateProductRequestModel request);
        Task<ApiResult<ProductViewModel>> UpdateProduct(UpdateProductRequestModel request);
        Task<ApiResult<bool>> DeleteProduct(Guid id);
        Task<PageResult<ProductViewModel>> GetProductsAsync(PagingRequestModel request, SearchRequestModel search, GetAllProductRequestModel model, bool seller = false);
        Task<ApiResult<ProductDetailsViewModel>> GetById(Guid id);
        Task<ApiResult<bool>> UpdateProductStatus(Guid id, bool status);
        Task<ApiResult<bool>> ApproveProduct(Guid id);
    }

    public class ProductService : IProductService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IRatingService _ratingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(GoodsExchangeDbContext context, IRatingService ratingService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _ratingService = ratingService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<bool>> ApproveProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Product does not exist.");
            }
            product.IsApproved = true;
            product.ApprovedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<ProductViewModel>> CreateProduct(CreateProductRequestModel request)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == request.ProductName);
            if (existingProduct != null)
            {
                return new ApiErrorResult<ProductViewModel>("Product name already exists.");
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId);
            if (category == null)
            {
                return new ApiErrorResult<ProductViewModel>("Category does not exist.");
            }

            var product = new Product()
            {
                ProductName = request.ProductName,
                Description = request.Description,
                ProductImageUrl = request.ProductImageUrl,
                Price = request.Price,
                IsActive = true,
                UploadDate = DateTime.Now,
                UserUploadId = Guid.Parse(_httpContextAccessor.GetCurrentUserId()),
                IsApproved = false,

                CategoryId = category.CategoryId
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == product.UserUploadId);

            var result = new ProductViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                ProductImageUrl = product.ProductImageUrl,
                Price = product.Price,
                IsActive = product.IsActive,
                UserUpload = user.UserName,
                UploadDate = product.UploadDate,
                ApprovedDate = product.ApprovedDate,
                CategoryName = _context.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId).CategoryName,
            };

            return new ApiSuccessResult<ProductViewModel>(result);
        }

        public async Task<ApiResult<bool>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Product does not exist.");
            }

            var userId = Guid.Parse(_httpContextAccessor.GetCurrentUserId());
            if (product.UserUploadId != userId)
            {
                return new ApiErrorResult<bool>("You do not have permission to delete this product.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true);
        }
        public async Task<PageResult<ProductViewModel>> GetProductsAsync(PagingRequestModel paging, SearchRequestModel search, GetAllProductRequestModel model, bool seller = false)
        {
            var query = _context.Products.Where(p => p.UserUpload.IsActive == true && p.IsApproved == true).AsQueryable();

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
            if (model.StartUploadDate != null)
            {
                query = query.Where(p => p.UploadDate >= model.StartUploadDate);
            }
            if (model.EndUploadDate != null)
            {
                query = query.Where(p => p.UploadDate <= model.EndUploadDate);
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

            if (seller == true)
            {
                query = query.Where(p => p.UserUploadId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            var data = await query.Skip((paging.PageIndex - 1) * paging.PageSize)
                                .Take(paging.PageSize)
                                .Select(product => new ProductViewModel()
                                {
                                    ProductId = product.ProductId,
                                    ProductName = product.ProductName,
                                    Description = product.Description,
                                    ProductImageUrl = product.ProductImageUrl,
                                    Price = product.Price,
                                    IsActive = product.IsActive,
                                    IsApproved = product.IsApproved,
                                    UserUpload = _context.Users.FirstOrDefault(u => u.UserId == product.UserUploadId).UserName,
                                    UploadDate = product.UploadDate,
                                    ApprovedDate = product.ApprovedDate,
                                    CategoryName = _context.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId).CategoryName
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

        public async Task<ApiResult<ProductViewModel>> UpdateProduct(UpdateProductRequestModel request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return new ApiErrorResult<ProductViewModel>("Product does not exist.");
            }

            var userId = Guid.Parse(_httpContextAccessor.GetCurrentUserId());
            if (product.UserUploadId != userId)
            {
                return new ApiErrorResult<ProductViewModel>("You do not have permission to update this product.");
            }

            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == request.ProductName);
            if (existingProduct != null)
            {
                return new ApiErrorResult<ProductViewModel>("Product name already exists.");
            }


            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.ProductImageUrl = request.ProductImageUrl;
            product.Price = request.Price.Value;
            product.CategoryId = request.CategoryId.Value;

            await _context.SaveChangesAsync();

            var result = new ProductViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                ProductImageUrl = product.ProductImageUrl,
                Price = product.Price,
                IsActive = product.IsActive,
                UserUpload = _context.Users.FirstOrDefault(u => u.UserId == product.UserUploadId).UserName,
                UploadDate = product.UploadDate,
                ApprovedDate = product.ApprovedDate,
                CategoryName = _context.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId).CategoryName
            };

            return new ApiSuccessResult<ProductViewModel>(result);
        }

        public async Task<ApiResult<bool>> UpdateProductStatus(Guid id, bool status)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Product does not exist.");
            }

            product.IsActive = status;
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<ProductDetailsViewModel>> GetById(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<ProductDetailsViewModel>("Product does not exist.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == product.UserUploadId);

            var result = new ProductDetailsViewModel()
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ProductImageUrl = product.ProductImageUrl,
                ApprovedDate = product.ApprovedDate,

                UserUpload = user.UserName,
                UserImageUrl = user.UserImageUrl,
                NumberOfRatings = _ratingService.CountNumberRatingOfUser(product.UserUpload.UserId).Result,
                AverageNumberStars = _ratingService.CountAverageNumberStarsOfUser(product.UserUpload.UserId).Result,
                UserPhoneNumber = user.PhoneNumber
            };

            return new ApiSuccessResult<ProductDetailsViewModel>(result);
        }
    }
}
