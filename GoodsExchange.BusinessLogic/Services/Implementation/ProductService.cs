using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Product;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Enums;
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

        private async Task<List<ProductImage>> AddListImages(string sellerName, List<IFormFile> request)
        {
            List<ProductImage> images = new List<ProductImage>();
            foreach (var image in request)
            {
                var img = new ProductImage()
                {
                    Caption = image.FileName,
                    DateCreated = DateTime.Now,
                    FileSize = image.Length,
                    ImagePath = await _serviceWrapper.FirebaseStorageServices.UploadProductImage(sellerName, image),
                };
                images.Add(img);
            }
            return images;
        }
        public async Task<ResponseModel<ProductListViewModel>> CreateProductAsync(CreateProductRequestModel request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var category = await _serviceWrapper.CategoryServices.GetCategoryAsync(request.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("Category does not exist.");
            }

            var product = new Product()
            {
                ProductName = request.ProductName,
                Description = request.Description,
                UploadDate = DateTime.Now,
                UserUploadId = Guid.Parse(_httpContextAccessor.GetCurrentUserId()),
                Status = Data.Enums.ProductStatus.AwaitingApproval,
                CategoryId = category.CategoryId,
                ProductImages = await AddListImages(user.UserName, request.Images)
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var result = new ProductListViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Status = (int)product.Status,
                UserUpload = user.UserName,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UploadDate = product.UploadDate,
                CategoryName = (await _serviceWrapper.CategoryServices.GetCategoryAsync(request.CategoryId)).CategoryName,
                UserUploadId = user.UserId,
                AverageNumberStars = await _serviceWrapper.RatingServices.CountAverageNumberStarsOfUser(user.UserId)
            };

            return new ResponseModel<ProductListViewModel>("The product was created successfully.", result);
        }

        public async Task<ResponseModel<bool>> DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
            }

            var userId = Guid.Parse(_httpContextAccessor.GetCurrentUserId());
            if (product.UserUploadId != userId)
            {
                throw new UnauthorizedException("You do not have permission to delete this product.");
            }

            if (product.Status == ProductStatus.AreExchanging)
            {
                throw new BadRequestException("You cannot delete this product because it is in the process of being exchanged.");
            }
            if (product.Status == ProductStatus.ExchangeSuccessful)
            {
                throw new BadRequestException("You cannot delete this product because the exchange is complete.");
            }
            else
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return new ResponseModel<bool>("The product was deleted successfully.", true);
        }
        public async Task<ResponseModel<PageResult<ProductListViewModel>>> GetProducts(PagingRequestModel request, string? keyword, ProductsRequestModel model, string role)
        {
            var query = _context.Products.Include(p => p.ProductImages)
                                        .Include(p => p.UserUpload)
                                        .Include(p => p.Category)
                                        .Where(p => p.UserUpload.IsActive == true)
                                        .AsQueryable();


            #region Searching
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.ProductName.Contains(keyword));
            }
            #endregion

            #region Filtering

            if (!string.IsNullOrEmpty(model.ProductName))
            {
                query = query.Where(p => p.ProductName.Contains(model.ProductName));
            }
            if (model.StartUploadDate != null)
            {
                query = query.Where(p => p.UploadDate >= model.StartUploadDate);
            }
            if (model.EndUploadDate != null)
            {
                query = query.Where(p => p.UploadDate <= model.EndUploadDate);
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

            #endregion

            if (role == SystemConstant.Roles.Guest)
            {
                query = query.Where(p => p.Status == Data.Enums.ProductStatus.Approved);
            }
            if (role == SystemConstant.Roles.Moderator)
            {
                query = query.Where(p => p.Status == Data.Enums.ProductStatus.AwaitingApproval);
            }

            var number = await query.CountAsync();
            var pages = (int)Math.Ceiling((double)number / request.PageSize);

            var data = await query.OrderByDescending(p => p.UploadDate)
                                .Skip((request.PageIndex - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .Select(product => new ProductListViewModel()
                                {
                                    ProductId = product.ProductId,
                                    ProductName = product.ProductName,
                                    Description = product.Description,
                                    Status = (int)product.Status,
                                    UserUpload = product.UserUpload.FirstName + " " + product.UserUpload.LastName,
                                    UserUploadId = product.UserUpload.UserId,
                                    ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                                    UploadDate = product.UploadDate,
                                    CategoryName = product.Category.CategoryName
                                })
                                .ToListAsync();
            foreach (var product in data)
            {
                product.AverageNumberStars = await _serviceWrapper.RatingServices.CountAverageNumberStarsOfUser(product.UserUploadId);
            }
            var result = new PageResult<ProductListViewModel>()
            {
                Items = data,
                TotalPage = pages,
                CurrentPage = request.PageIndex
            };
            return new ResponseModel<PageResult<ProductListViewModel>>(result);
        }

        public async Task<ResponseModel<ProductListViewModel>> UpdateProductAsync(UpdateProductRequestModel request)
        {
            var product = await _context.Products.Include(p => p.Category)
                                                .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
            }

            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            if (product.UserUploadId != user.UserId)
            {
                throw new UnauthorizedException("You do not have permission to update this product.");
            }

            var category = await _serviceWrapper.CategoryServices.GetCategoryAsync(request.CategoryId.Value);
            if (category == null)
            {
                throw new NotFoundException("This category does not exist.");
            }
            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId.Value;
            product.ProductImages = await AddListImages(user.UserName, request.Images);

            // Reset status as new product
            product.Status = Data.Enums.ProductStatus.AwaitingApproval;
            await _context.SaveChangesAsync();

            var result = new ProductListViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Status = (int)product.Status,
                UserUpload = user.UserName,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UploadDate = product.UploadDate,
                CategoryName = product.Category.CategoryName,
                UserUploadId = user.UserId,
                AverageNumberStars = await _serviceWrapper.RatingServices.CountAverageNumberStarsOfUser(user.UserId)
            };

            return new ResponseModel<ProductListViewModel>("The product was updated successfully.", result);
        }

        public async Task<ResponseModel<bool>> UpdateProductStatusAsync(Guid id, ProductStatus status)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
            }

            product.Status = status;
            await _context.SaveChangesAsync();

            return new ResponseModel<bool>("The product status was updated successfully.", true);
        }

        public async Task<ResponseModel<ProductDetailViewModel>> GetProductDetailAsync(Guid id)
        {
            var product = await _context.Products
                                        .Include(p => p.ProductImages)
                                        .Include(p => p.UserUpload)
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
            }

            var result = new ProductDetailViewModel()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                //ApprovedDate = product.ApprovedDate,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UserUploadId = product.UserUpload.UserId,
                UserUpload = product.UserUpload.LastName + " " + product.UserUpload.FirstName,
                UserImageUrl = product.UserUpload.UserImageUrl,
                NumberOfRatings = _serviceWrapper.RatingServices.CountNumberRatingOfUser(product.UserUpload.UserId).Result,
                AverageNumberStars = _serviceWrapper.RatingServices.CountAverageNumberStarsOfUser(product.UserUpload.UserId).Result,
                UserPhoneNumber = product.UserUpload.PhoneNumber,
                CategoryName = product.Category.CategoryName
            };

            return new ResponseModel<ProductDetailViewModel>("The product details were retrieved successfully.", result);
        }
        public async Task<Product> GetProductAsync(Guid id)
        {
            var product = await _context.Products.Include(p => p.ProductImages)
                                                .Include(p => p.UserUpload)
                                                .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
            }
            return product;
        }

        public async Task<bool> IsProductBelongToSellerAsync(Guid productId, Guid sellerId)
        {
            var seller = await _context.Users.Include(u => u.Products).FirstOrDefaultAsync(u => u.UserId == sellerId);
            return seller != null && seller.Products.Any(p => p.ProductId == productId);
        }

        public async Task<ResponseModel<PageResult<UserProductListViewModel>>> GetProductsForUserAsync(PagingRequestModel request)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var query = _context.Products.Include(p => p.ProductImages)
                                       .Include(p => p.UserUpload)
                                       .Include(p => p.Category)
                                       .Where(p => p.UserUploadId == user.UserId)
                                       .AsQueryable();

            var numbers = await query.CountAsync();
            var pages = (int)Math.Ceiling((double)numbers / request.PageSize);

            var products = await query.OrderByDescending(p => p.UploadDate)
                              .Skip((request.PageIndex - 1) * request.PageSize)
                              .Take(request.PageSize)
                              .ToListAsync();

            var data = products.Select(product => new UserProductListViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Status = product.Status.ToString(),
                UserUpload = product.UserUpload.FirstName + " " + product.UserUpload.LastName,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UploadDate = product.UploadDate,
                CategoryName = product.Category.CategoryName
            }).ToList();

            var result = new PageResult<UserProductListViewModel>()
            {
                Items = data,
                TotalPage = pages,
                CurrentPage = request.PageIndex
            };
            return new ResponseModel<PageResult<UserProductListViewModel>>(result);
        }

        public async Task<ResponseModel<UserProductDetailViewModel>> GetUserProductDetailAsync(Guid id)
        {
            var product = await _context.Products
                                       .Include(p => p.ProductImages)
                                       .Include(p => p.UserUpload)
                                       .Include(p => p.Category)
                                       .Include(p => p.ExchangeRequestsSent)
                                       .Include(p => p.ExchangeRequestsReceived)
                                       .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
            }

            var result = new UserProductDetailViewModel()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                CategoryName = product.Category.CategoryName,
                SentRequests = await _serviceWrapper.ExchangeRequestService.GetSentExchangeRequestsAsync(),
                ReceivedRequests = await _serviceWrapper.ExchangeRequestService.GetReceivedExchangeRequestsAsync()
            };

            return new ResponseModel<UserProductDetailViewModel>("The product details were retrieved successfully.", result);
        }

        public async Task<ResponseModel<PageResult<UserProductListViewModel>>> GetProductsForUserProfileAsync(PagingRequestModel request, Guid id)
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist.");
            }

            var query = _context.Products.Include(p => p.ProductImages)
                                       .Include(p => p.UserUpload)
                                       .Include(p => p.Category)
                                       .Where(p => p.UserUploadId == id && p.Status == ProductStatus.Approved)
                                       .AsQueryable();

            var numbers = await query.CountAsync();
            var pages = (int)Math.Ceiling((double)numbers / request.PageSize);

            var products = await query.OrderByDescending(p => p.UploadDate)
                              .Skip((request.PageIndex - 1) * request.PageSize)
                              .Take(request.PageSize)
                              .ToListAsync();

            var data = products.Select(product => new UserProductListViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Status = product.Status.ToString(),
                UserUpload = product.UserUpload.FirstName + " " + product.UserUpload.LastName,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UploadDate = product.UploadDate,
                CategoryName = product.Category.CategoryName
            }).ToList();

            var result = new PageResult<UserProductListViewModel>()
            {
                Items = data,
                TotalPage = pages,
                CurrentPage = request.PageIndex
            };
            return new ResponseModel<PageResult<UserProductListViewModel>>(result);
        }
    }
}
