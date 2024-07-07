using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
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

        public async Task<ResponseModel<bool>> ApproveProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException("The product does not exist.");
            }
            product.IsApproved = true;
            product.ApprovedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ResponseModel<bool>("The product was approved successfully.", true);
        }
        public async Task<ResponseModel<bool>> DenyProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException("The product does not exist.");
            }
            product.IsApproved = false;
            product.IsActive = false;
            product.ApprovedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ResponseModel<bool>("The product was denied successfully.", true);
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
        public async Task<ResponseModel<ProductViewModel>> CreateProduct(CreateProductRequestModel request)
        {
            var seller = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == request.ProductName);
            if (existingProduct != null)
            {
                throw new BadRequestException("Product name already exists.");
            }

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

            return new ResponseModel<ProductViewModel>("The product was created successfully.", result);
        }

        public async Task<ResponseModel<bool>> DeleteProduct(Guid id)
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
        public async Task<ResponseModel<PageResult<ProductViewModel>>> GetProducts(PagingRequestModel request, string? keyword, ProductsRequestModel model, bool seller = false, bool moderator = false)
        {
            var query = _context.Products.Include(p => p.ProductImages)
                                        .Include(p => p.UserUpload)
                                        .Include(p => p.Category)
                                        .Where(p => p.UserUpload.IsActive == true && p.IsApproved == true)
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
            return new ResponseModel<PageResult<ProductViewModel>>(result);
        }

        public async Task<ResponseModel<ProductViewModel>> UpdateProduct(UpdateProductRequestModel request)
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

            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == request.ProductName);
            if (existingProduct != null)
            {
                throw new BadRequestException("Product name already exists.");
            }

            var category = await _serviceWrapper.CategoryServices.GetById(request.CategoryId.Value);
            if (category == null)
            {
                throw new NotFoundException("This category does not exist.");
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

            return new ResponseModel<ProductViewModel>("The product was updated successfully.", result);
        }

        public async Task<ResponseModel<bool>> UpdateProductStatus(Guid id, bool status)
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

        public async Task<ResponseModel<ProductDetailsViewModel>> GetById(Guid id)
        {
            var product = await _context.Products
                                        .Include(p => p.ProductImages)
                                        .Include(p => p.UserUpload)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
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

            return new ResponseModel<ProductDetailsViewModel>("The product details were retrieved successfully.", result);
        }
        public async Task<Product> GetProductAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
            }
            return product;
        }

        public async Task<bool> IsProductBelongToSeller(Guid productId, Guid sellerId)
        {
            var seller = await _context.Users.Include(u => u.Products).FirstOrDefaultAsync(u => u.UserId == sellerId);
            return seller != null && seller.Products.Any(p => p.ProductId == productId);
        }

        public async Task<ResponseModel<PurchaseProductViewModel>> PurchaseProduct(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                throw new NotFoundException("Product does not exist.");
            }
            var buyer = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            return null;
        }
    }
}
