using GoodsExchange.BusinessLogic.Common;
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
        public async Task<ApiResult<RatingViewModel>> SendRating(CreateRatingRequestModel request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            if (await _serviceWrapper.UserServices.GetUserAsync(request.ReceiverId) == null)
            {
                return new ApiErrorResult<RatingViewModel>("This user does not exist.");
            }

            if (!(await _serviceWrapper.RoleServices.HasPermissionToReportAndRating(user.UserId, request.ReceiverId)))
            {
                return new ApiErrorResult<RatingViewModel>("You can not rate this user.");
            }

            if (await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId) == null)
            {
                return new ApiErrorResult<RatingViewModel>("This product does not exist.");
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

