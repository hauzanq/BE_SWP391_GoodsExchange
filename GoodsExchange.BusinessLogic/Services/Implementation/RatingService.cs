using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Rating;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class RatingService : IRatingService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceWrapper _serviceWrapper;

        public RatingService(GoodsExchangeDbContext context, IHttpContextAccessor httpContextAccessor, IServiceWrapper serviceWrapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceWrapper = serviceWrapper;
        }

        public async Task<float> CountAverageNumberStarsOfUser(Guid id)
        {
            var numberStarts = await _context.Ratings.Where(r => r.ReceiverId == id).Select(r => r.NumberStars).SumAsync();
            var numberRatings = await _context.Ratings.Where(r => r.ReceiverId == id).Select(r => r.ReceiverId).CountAsync();

            float average = 0;
            if (numberRatings > 0)
            {
                average = (float)Math.Round(numberStarts * 1.0f / numberRatings, 1);
            }
            return average;
        }

        public async Task<int> CountNumberRatingOfUser(Guid id)
        {
            var numberRatings = await _context.Ratings.Where(r => r.ReceiverId == id).Select(r => r.ReceiverId).CountAsync();
            return numberRatings;
        }

        public async Task<PageResult<RatingViewModel>> GetAll(PagingRequestModel paging, RatingsRequestModel request)
        {
            var seller = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var query = _context.Ratings.Include(r => r.Receiver).ThenInclude(u => u.Products).ThenInclude(p => p.Category).AsQueryable();

            // Products of current seller
            query = query.Where(r => r.Product.UserUpload.UserId == seller.UserId);

            if (request.FromDate != null)
            {
                query = query.Where(r => r.CreateDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                query = query.Where(r => r.CreateDate <= request.ToDate);
            }

            if (request.CategoryId != null)
            {
                query = query.Where(r => r.Product.CategoryId == request.CategoryId);
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            var data = query.Select(rating => new RatingViewModel()
            {
                RatingId = rating.RatingId,
                CreateDate = rating.CreateDate,
                Feedback = rating.Feedback,
                NumberStars = rating.NumberStars,
                ProductId = rating.ProductId,
                ProductName = _context.Products.FirstOrDefault(p => p.ProductId == rating.ProductId).ProductName,
                Sender = _context.Users.FirstOrDefault(u => u.UserId == rating.SenderId).UserName,
                Receiver = seller.FirstName + "" + seller.LastName
            }).ToList();

            var result = new PageResult<RatingViewModel>()
            {
                Items = data,
                TotalPage = totalPages,
                CurrentPage = paging.PageIndex
            };

            return result;

        }

        public async Task<ApiResult<RatingViewModel>> GetById(Guid id)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.RatingId == id);
            if (rating == null)
            {
                return new ApiErrorResult<RatingViewModel>("This rating does not exist.");
            }

            var result = new RatingViewModel()
            {
                RatingId = rating.RatingId,
                CreateDate = rating.CreateDate,
                Feedback = rating.Feedback,
                NumberStars = rating.NumberStars,
                ProductId = rating.ProductId,
                ProductName = _context.Products.FirstOrDefault(p => p.ProductId == rating.ProductId).ProductName,
                Sender = _context.Users.FirstOrDefault(u => u.UserId == rating.SenderId).UserName,
                Receiver = _context.Users.FirstOrDefault(u => u.UserId == rating.ReceiverId).UserName,
            };
            return new ApiSuccessResult<RatingViewModel>(result);
        }
        #region For Send Rating
        private async Task<bool> UserExists(Guid id)
        {
            //return (await _userService.GetById(id)).Data != null;
            return (await _serviceWrapper.UserServices.GetById(id)).Data != null;
        }
        private async Task<bool> HasPermissionToRating(Guid from, Guid to)
        {
            var receiver = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserId == to);
            if (receiver.UserId == from)
            {
                return false;
            }
            var roles = receiver.UserRoles.Select(u => u.Role.RoleName).ToList();
            if (roles.Any(r => r.Contains(SystemConstant.Roles.Moderator) || r.Contains(SystemConstant.Roles.Administrator) || r.Contains(SystemConstant.Roles.Buyer)))
            {
                return false;
            }
            return true;
        }
        private async Task<bool> ProductExists(Guid id)
        {
            //return (await _productService.GetById(id)).Data != null;
            return (await _serviceWrapper.ProductServices.GetById(id)).Data != null;
        }
        private async Task<bool> IsProductBelongToSeller(Guid sellerId, Guid productId)
        {
            var seller = _context.Users.Include(u => u.Products).FirstOrDefault(u => u.UserId == sellerId);
            return seller != null && seller.Products.Any(p => p.ProductId == productId);
        }
        #endregion
        public async Task<ApiResult<RatingViewModel>> SendRating(CreateRatingRequestModel request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            if (!await UserExists(request.ReceiverId))
            {
                return new ApiErrorResult<RatingViewModel>("The rating receiver does not exist.");
            }

            if (!await HasPermissionToRating(user.UserId, request.ReceiverId))
            {
                return new ApiErrorResult<RatingViewModel>("You can not rate this user.");
            }

            if (!await ProductExists(request.ProductId))
            {
                return new ApiErrorResult<RatingViewModel>("This product does not exist.");
            }

            var ratedProduct = await _context.Ratings.AnyAsync(r => r.ProductId == request.ProductId);
            if (ratedProduct)
            {
                return new ApiErrorResult<RatingViewModel>("You are only allowed to rate each product once.");
            }

            if (!(await IsProductBelongToSeller(request.ReceiverId, request.ProductId)))
            {
                return new ApiErrorResult<RatingViewModel>("This product is not belong to this seller.");
            }

            var rating = new Rating()
            {
                NumberStars = request.NumberStars,
                Feedback = request.Feedback,
                CreateDate = DateTime.Now,
                SenderId = user.UserId,
                ReceiverId = request.ReceiverId,
                ProductId = request.ProductId
            };

            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            var result = new RatingViewModel()
            {
                RatingId = rating.RatingId,
                CreateDate = rating.CreateDate,
                Feedback = rating.Feedback,
                NumberStars = rating.NumberStars,
                ProductId = rating.ProductId,
                ProductName = _context.Products.FirstOrDefault(p => p.ProductId == rating.ProductId).ProductName,
                Sender = _context.Users.FirstOrDefault(u => u.UserId == rating.SenderId).UserName,
                Receiver = _context.Users.FirstOrDefault(u => u.UserId == rating.ReceiverId).UserName,
            };
            return new ApiSuccessResult<RatingViewModel>(result);
        }
    }
}

