using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
using GoodsExchange.BusinessLogic.Constants;
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

        public async Task<ResponseModel<bool>> ApproveProductAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            product.IsApproved = true;
            product.IsReviewed = true;
            product.ApprovedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ResponseModel<bool>("Product approved successfully.");
        }
        public async Task<ResponseModel<bool>> DenyProductAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            product.IsReviewed = true;
            await _context.SaveChangesAsync();

            return new ResponseModel<bool>("Product denied successfully.");
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
        public async Task<ResponseModel<ProductListViewModel>> CreateProductAsync(CreateProductRequestModel request)
        {
            var seller = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var category = await _serviceWrapper.CategoryServices.GetCategoryAsync(request.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("Category does not exist.");
            }

            var sellerFullName = await _serviceWrapper.UserServices.GetUserFullNameAsync(seller.UserId);

            var product = new Product()
            {
                ProductName = request.ProductName,
                Description = request.Description,
                IsActive = true,
                UploadDate = DateTime.Now,
                UserUploadId = Guid.Parse(_httpContextAccessor.GetCurrentUserId()),
                IsApproved = false,
                IsReviewed = false,
                CategoryId = category.CategoryId,
                ProductImages = await AddListImages(sellerFullName, request)
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            var user = await _serviceWrapper.UserServices.GetUserAsync(product.UserUploadId);

            var result = new ProductListViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                IsActive = product.IsActive,
                UserUpload = user.UserName,
                IsApproved = product.IsApproved,
                IsReviewed = product.IsReviewed,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UploadDate = product.UploadDate,
                ApprovedDate = product.ApprovedDate,
                CategoryName = (await _serviceWrapper.CategoryServices.GetById(request.CategoryId)).Data.CategoryName,
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

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

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

            #endregion

            if (role == SystemConstant.Roles.Guest)
            {
                query = query.Where(p => p.IsApproved == true);
            }

            if (role == SystemConstant.Roles.Moderator)
            {
                query = query.Where(p => p.IsApproved == false && p.IsReviewed == false);
            }

            if (role == SystemConstant.Roles.Moderator)
            {
                query = query.Where(p => p.IsApproved == false && p.IsReviewed == false);
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
                                    IsActive = product.IsActive,
                                    IsApproved = product.IsApproved,
                                    IsReviewed = product.IsReviewed,
                                    UserUpload = product.UserUpload.FirstName + " " + product.UserUpload.LastName,
                                    ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                                    UploadDate = product.UploadDate,
                                    ApprovedDate = product.ApprovedDate,
                                    CategoryName = product.Category.CategoryName
                                })
                                .ToListAsync();

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

            var category = await _serviceWrapper.CategoryServices.GetById(request.CategoryId.Value);
            if (category == null)
            {
                throw new NotFoundException("This category does not exist.");
            }
            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId.Value;

            await _context.SaveChangesAsync();

            var result = new ProductListViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                IsActive = product.IsActive,
                IsReviewed = product.IsReviewed,
                IsApproved = product.IsApproved,
                UserUpload = user.UserName,
                ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                UploadDate = product.UploadDate,
                ApprovedDate = product.ApprovedDate,
                CategoryName = product.Category.CategoryName,
            };

            return new ResponseModel<ProductListViewModel>("The product was updated successfully.", result);
        }

        public async Task<ResponseModel<bool>> UpdateProductStatusAsync(Guid id, bool status)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
            }

            product.IsActive = status;
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
                ApprovedDate = product.ApprovedDate,
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
            var product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.ProductId == id);
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

        private async Task<string> GetProductStatusAsync(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new NotFoundException("The product does not exist.");
            }

            if (await _serviceWrapper.TransactionService.IsProductInTransactionAsync(productId))
            {
                return SystemConstant.ProductStatus.ExchangeSuccessful;
            }

            if (await _serviceWrapper.ExchangeRequestService.IsProductInExchangeProcessingAsync(productId))
            {
                return SystemConstant.ProductStatus.AreExchanging;
            }

            if (!product.IsActive)
            {
                return SystemConstant.ProductStatus.Hidden;
            }

            if (product.IsApproved && product.ApprovedDate != DateTime.MinValue)
            {
                return SystemConstant.ProductStatus.Approved;
            }

            if (!product.IsApproved && product.ApprovedDate != DateTime.MinValue)
            {
                return SystemConstant.ProductStatus.Rejected;
            }

            return SystemConstant.ProductStatus.AwaitingApproval;
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

            var data = new List<UserProductListViewModel>();

            foreach (var product in products)
            {
                var status = await GetProductStatusAsync(product.ProductId);

                data.Add(new UserProductListViewModel()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Status = status,
                    UserUpload = product.UserUpload.FirstName + " " + product.UserUpload.LastName,
                    ProductImageUrl = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                    UploadDate = product.UploadDate,
                    ApprovedDate = product.ApprovedDate,
                    CategoryName = product.Category.CategoryName
                });
            }

            var result = new PageResult<UserProductListViewModel>()
            {
                Items = data,
                TotalPage = pages,
                CurrentPage = request.PageIndex
            };
            return new ResponseModel<PageResult<UserProductListViewModel>>(result);
        }

        public Task<ResponseModel<PageResult<UserProductListViewModel>>> GetRejectedExchangeRequestsAsync(PagingRequestModel request)
        {
            throw new NotImplementedException();
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
    }
}
