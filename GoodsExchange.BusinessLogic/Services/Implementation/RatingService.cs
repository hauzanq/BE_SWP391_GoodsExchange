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

        public async Task<PageResult<RatingViewModel>> GetRatings(PagingRequestModel paging, RatingsRequestModel request)
        {
            var seller = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var query = _context.Ratings.Where(r => r.ReceiverId == seller.UserId)
                            .Include(r=>r.Sender)
                            .Include(r=>r.Receiver)
                            .Include(r=>r.Product)
                            .AsQueryable();

            #region Filter
            if (request.MinStars != null)
            {
                query = query.Where(r => r.NumberStars >= request.MinStars);
            }

            if (request.MaxStars != null)
            {
                query = query.Where(r => r.NumberStars <= request.MaxStars);
            }

            if (!string.IsNullOrEmpty(request.FeedbackSearchTerm))
            {
                query = query.Where(r => r.Feedback.Contains(request.FeedbackSearchTerm));
            }

            if (request.FromDate != null)
            {
                query = query.Where(r => r.CreateDate >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                query = query.Where(r => r.CreateDate <= request.ToDate);
            }

            if (request.SenderId != null)
            {
                if ((await _serviceWrapper.UserServices.GetUserAsync(request.SenderId.Value)) != null)
                {
                    query = query.Where(r => r.SenderId == request.SenderId);
                }
            }

            if (request.ProductId != null)
            {
                if ((await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId.Value)) != null)
                {
                    query = query.Where(r => r.ProductId == request.ProductId);
                }
            }

            #endregion

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            var data = await query.Skip((paging.PageIndex - 1) * paging.PageSize)
                                .Take(paging.PageSize)
                                .Select(rating => new RatingViewModel()
                                {
                                    RatingId = rating.RatingId,
                                    CreateDate = rating.CreateDate,
                                    Feedback = rating.Feedback,
                                    NumberStars = rating.NumberStars,
                                    ProductId = rating.ProductId,
                                    ProductName = rating.Product.ProductName,
                                    SenderId = rating.SenderId,
                                    SenderName = rating.Sender.FirstName + " " + rating.Sender.LastName,
                                })
                                .ToListAsync();

            var result = new PageResult<RatingViewModel>()
            {
                Items = data,
                TotalPage = totalPages,
                CurrentPage = paging.PageIndex
            };
            return result;
        }

        public async Task<ApiResult<RatingViewModel>> GetRatingById(Guid id)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.RatingId == id);
            if (rating == null)
            {
                return new ApiErrorResult<RatingViewModel>("This rating does not exist.");
            }

            var result = new RatingViewModel()
            {
                RatingId = rating.RatingId,
                NumberStars = rating.NumberStars,
                Feedback = rating.Feedback,
                CreateDate = rating.CreateDate,
                SenderId = rating.SenderId,
                SenderName = await _serviceWrapper.UserServices.GetUserFullNameAsync(rating.SenderId),
                ProductId = rating.ProductId,
                ProductName = (await _serviceWrapper.ProductServices.GetProductAsync(rating.ProductId)).ProductName,
            };
            return new ApiSuccessResult<RatingViewModel>(result);
        }
        public async Task<ApiResult<RatingViewModel>> SendRating(CreateRatingRequestModel request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var receiver = await _serviceWrapper.UserServices.GetUserByProductId(request.ProductId);

            if (await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId) == null)
            {
                return new ApiErrorResult<RatingViewModel>("This product does not exist.");
            }
            if (await _serviceWrapper.ProductServices.IsProductBelongToSeller(request.ProductId, user.UserId))
            {
                return new ApiErrorResult<RatingViewModel>("This product belongs to you so you cannot rating this product.");
            }
            var rating = new Rating()
            {
                NumberStars = request.NumberStars,
                Feedback = request.Feedback,
                CreateDate = DateTime.Now,
                SenderId = user.UserId,
                ReceiverId = receiver.UserId,
                ProductId = request.ProductId
            };

            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            var result = new RatingViewModel()
            {
                RatingId = rating.RatingId,
                NumberStars = rating.NumberStars,
                Feedback = rating.Feedback,
                CreateDate = rating.CreateDate,
                SenderId = rating.SenderId,
                SenderName = await _serviceWrapper.UserServices.GetUserFullNameAsync(rating.SenderId),
                ProductId = rating.ProductId,
                ProductName = (await _serviceWrapper.ProductServices.GetProductAsync(rating.ProductId)).ProductName,
            };
            return new ApiSuccessResult<RatingViewModel>(result);
        }
    }
}

