using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceWrapper _serviceWrapper;
        public ProductService(GoodsExchangeDbContext context, IHttpContextAccessor httpContextAccessor, IServiceWrapper serviceWrapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceWrapper = serviceWrapper;
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
        public async Task<ApiResult<bool>> DenyProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>("Product does not exist.");
            }
            product.IsApproved = false;
            product.ApprovedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true);
        }
        private async Task<List<ProductImage>> AddListImages(string sellerName, CreateProductRequestModel request)
        {
            List<ProductImage> images = new List<ProductImage>();
            foreach (var image in request.Images)
            {
                var img = new ProductImage()
                {
                    Caption = image.FileName,
                    DateCreated = DateTime.Now,
                    FileSize = image.Length,
                    ImagePath = await _serviceWrapper.FirebaseStorageServices.UploadProductImage(sellerName, request.ProductName, image),
                };
                images.Add(img);
            }
            return images;
        }
        public async Task<ApiResult<ProductViewModel>> CreateProduct(CreateProductRequestModel request)
        {
            var seller = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == request.ProductName);
            if (existingProduct != null)
            {
                return new ApiErrorResult<ProductViewModel>("Product name already exists.");
            }

            var category = await _serviceWrapper.CategoryServices.GetCategoryAsync(request.CategoryId);
            if (category == null)
            {
                return new ApiErrorResult<ProductViewModel>("Category does not exist.");
            }

            var sellerFullName = await _serviceWrapper.UserServices.GetUserFullNameAsync(seller.UserId);

            var product = new Product()
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                IsActive = true,
                UploadDate = DateTime.Now,
                UserUploadId = Guid.Parse(_httpContextAccessor.GetCurrentUserId()),
                IsApproved = false,
                CategoryId = category.CategoryId,
                ProductImages = await AddListImages(sellerFullName, request)
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var user = await _serviceWrapper.UserServices.GetUserAsync(product.UserUploadId);

            var result = new ProductViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive,
                UserUpload = user.UserName,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UploadDate = product.UploadDate,
                ApprovedDate = product.ApprovedDate,
                CategoryName = (await _serviceWrapper.CategoryServices.GetById(request.CategoryId)).Data.CategoryName,
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
        public async Task<PageResult<ProductViewModel>> GetAll(PagingRequestModel request, SearchRequestModel search, GetAllProductRequestModel model, bool seller = false, bool moderator = false)
        {
            var query = _context.Products.Include(p => p.ProductImages)
                                        .Include(p => p.UserUpload)
                                        .Include(p => p.Category)
                                        .Where(p => p.UserUpload.IsActive == true && p.IsApproved == true)
                                        .AsQueryable();

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
                query = _context.Products.Where(p => p.UserUpload.IsActive == true).AsQueryable();

                query = query.Where(p => p.UserUploadId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            }

            if (moderator == true)
            {
                query = _context.Products.Where(p => p.UserUpload.IsActive == true && p.IsApproved == false).AsQueryable();
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .Select(product => new ProductViewModel()
                                {
                                    ProductId = product.ProductId,
                                    ProductName = product.ProductName,
                                    Description = product.Description,
                                    Price = product.Price,
                                    IsActive = product.IsActive,
                                    IsApproved = product.IsApproved,
                                    UserUpload = product.UserUpload.FirstName + " " + product.UserUpload.LastName,
                                    ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                                    UploadDate = product.UploadDate,
                                    ApprovedDate = product.ApprovedDate,
                                    CategoryName = product.Category.CategoryName
                                })
                                .ToListAsync();

            var result = new PageResult<ProductViewModel>()
            {
                Items = data,
                TotalPage = totalPages,
                CurrentPage = request.PageIndex
            };
            return result;
        }

        public async Task<ApiResult<ProductViewModel>> UpdateProduct(UpdateProductRequestModel request)
        {
            var product = await _context.Products.Include(p => p.Category)
                                                .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);
            if (product == null)
            {
                return new ApiErrorResult<ProductViewModel>("Product does not exist.");
            }

            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            if (product.UserUploadId != user.UserId)
            {
                return new ApiErrorResult<ProductViewModel>("You do not have permission to update this product.");
            }

            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == request.ProductName);
            if (existingProduct != null)
            {
                return new ApiErrorResult<ProductViewModel>("Product name already exists.");
            }

            var category = await _serviceWrapper.CategoryServices.GetById(request.CategoryId.Value);
            if (category == null)
            {
                return new ApiErrorResult<ProductViewModel>("This category does not exist.");
            }
            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.Price = request.Price.Value;
            product.CategoryId = request.CategoryId.Value;

            await _context.SaveChangesAsync();

            var result = new ProductViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive,
                UserUpload = user.UserName,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UploadDate = product.UploadDate,
                ApprovedDate = product.ApprovedDate,
                CategoryName = product.Category.CategoryName,
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
            var product = await _context.Products
                                        .Include(p => p.ProductImages)
                                        .Include(p => p.UserUpload)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return new ApiErrorResult<ProductDetailsViewModel>("Product does not exist.");
            }

            var result = new ProductDetailsViewModel()
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ApprovedDate = product.ApprovedDate,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UserUploadId = product.UserUpload.UserId,
                UserUpload = product.UserUpload.FirstName + " " + product.UserUpload.LastName,
                UserImageUrl = product.UserUpload.UserImageUrl,
                NumberOfRatings = _serviceWrapper.RatingServices.CountNumberRatingOfUser(product.UserUpload.UserId).Result,
                AverageNumberStars = _serviceWrapper.RatingServices.CountAverageNumberStarsOfUser(product.UserUpload.UserId).Result,
                UserPhoneNumber = product.UserUpload.PhoneNumber
            };

            return new ApiSuccessResult<ProductDetailsViewModel>(result);
        }
        public Task<Product> GetProductAsync(Guid id)
        {
            var product = _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public async Task<bool> IsProductBelongToSeller(Guid productId, Guid sellerId)
        {
            var seller = await _context.Users.Include(u => u.Products).FirstOrDefaultAsync(u => u.UserId == sellerId);
            return seller != null && seller.Products.Any(p => p.ProductId == productId);
        }
    }
}
