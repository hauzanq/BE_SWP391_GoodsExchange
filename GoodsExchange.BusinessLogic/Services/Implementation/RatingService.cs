using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
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

        public async Task<ResponseModel<PageResult<RatingViewModel>>> GetRatings(PagingRequestModel paging, RatingsRequestModel request, Guid? userid = null)
        {
            var query = _context.Ratings
                            .Include(r => r.Sender)
                            .Include(r => r.Receiver)
                            .Include(r => r.Product)
                            .AsQueryable();

            if (userid != null)
            {
                query = query.Where(r => r.ReceiverId == userid);
            }

            #region Filter
            if (request.MinStars != null)
            {
                query = query.Where(r => r.NumberStars >= request.MinStars);
            }

            if (request.MaxStars != null)
            {
                query = query.Where(r => r.NumberStars <= request.MaxStars);
            }

            if (request.FromDate != null)
            {
                query = query.Where(r => r.DateCreated >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                query = query.Where(r => r.DateCreated <= request.ToDate);
            }
            #endregion

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            var data = await query.Skip((paging.PageIndex - 1) * paging.PageSize)
                                .Take(paging.PageSize)
                                .Select(rating => new RatingViewModel()
                                {
                                    RatingId = rating.RatingId,
                                    CreateDate = rating.DateCreated,
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
            return new ResponseModel<PageResult<RatingViewModel>>(result);
        }

        public async Task<ResponseModel<RatingViewModel>> GetRatingById(Guid id)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.RatingId == id);
            if (rating == null)
            {
                throw new NotFoundException("This rating does not exist.");
            }

            var result = new RatingViewModel()
            {
                RatingId = rating.RatingId,
                NumberStars = rating.NumberStars,
                Feedback = rating.Feedback,
                CreateDate = rating.DateCreated,
                SenderId = rating.SenderId,
                SenderName = await _serviceWrapper.UserServices.GetUserFullNameAsync(rating.SenderId),
                ProductId = rating.ProductId,
                ProductName = (await _serviceWrapper.ProductServices.GetProductAsync(rating.ProductId)).ProductName,
            };
            return new ResponseModel<RatingViewModel>("The rating was retrieved successfully.", result);
        }
        private async Task<bool> IsCustomerRatingProduct(Guid userid, Guid productid)
        {
            return await _context.Ratings.AnyAsync(r => r.SenderId == userid && r.ProductId == productid);
        }
        public async Task<ResponseModel<RatingViewModel>> SendRating(CreateRatingRequestModel request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var receiver = await _serviceWrapper.UserServices.GetUserByProductId(request.ProductId);

            if (await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId) == null)
            {
                throw new NotFoundException("This product does not exist.");
            }
            if (await _serviceWrapper.ProductServices.IsProductBelongToSellerAsync(request.ProductId, user.UserId))
            {
                throw new BadRequestException("This product belongs to you so you cannot rating this product.");
            }
            if (await IsCustomerRatingProduct(user.UserId, request.ProductId))
            {
                throw new BadRequestException("You have rated this product.");
            }
            var rating = new Rating()
            {
                NumberStars = request.NumberStars,
                Feedback = request.Feedback,
                DateCreated = DateTime.Now,
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
                CreateDate = rating.DateCreated,
                SenderId = rating.SenderId,
                SenderName = await _serviceWrapper.UserServices.GetUserFullNameAsync(rating.SenderId),
                ProductId = rating.ProductId,
                ProductName = (await _serviceWrapper.ProductServices.GetProductAsync(rating.ProductId)).ProductName,
            };
            return new ResponseModel<RatingViewModel>("The rating was submitted successfully.", result);
        }

        public async Task<bool> IsUserRatedOnProduct(Guid userid, Guid productid)
        {
            var result = _context.Ratings.Any(r => r.SenderId == userid && r.ProductId == productid);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}

