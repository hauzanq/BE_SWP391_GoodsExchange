using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IRatingService
    {
       Task<int> CountNumberRatingOfUser(Guid id);
       Task<float> CountAverageNumberStarsOfUser(Guid id);
    }

    public class RatingService : IRatingService
    {
        private readonly GoodsExchangeDbContext _context;

        public RatingService(GoodsExchangeDbContext context)
        {
            _context = context;
        }

        public async Task<float> CountAverageNumberStarsOfUser(Guid id)
        {
            var numberStarts = await _context.Ratings.Where(r => r.TargetUserId == id).Select(r => r.NumberStars).SumAsync();
            var numberRatings = await _context.Ratings.Where(r => r.TargetUserId == id).Select(r => r.TargetUserId).CountAsync();

            float average = 0;
            if (numberRatings > 0)
            {
                 average = (float)Math.Round(numberStarts * 1.0f / numberRatings, 1);
            }
            return average;
        }

        public async Task<int> CountNumberRatingOfUser(Guid id)
        {
            var numberRatings = await _context.Ratings.Where(r => r.TargetUserId == id).Select(r => r.TargetUserId).CountAsync();
            return numberRatings;
        }
    }
}
