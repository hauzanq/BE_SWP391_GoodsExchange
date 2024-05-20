using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.ViewModels;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IRatingService
    {
        Task<RatingViewModel> CreateRating(CreateRatingRequestModel ratingCreate);
        Task<RatingViewModel> UpdateRating(UpdateRatingRequestModel ratingUpdate);
        Task<bool> DeleteRating(int idTmp);
        Task<List<RatingViewModel>> GetAll();
        Task<RatingViewModel> GetById(int idTmp);
    }

    public class RatingService : IRatingService
    {
        public Task<RatingViewModel> CreateRating(CreateRatingRequestModel ratingCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRating(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<List<RatingViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<RatingViewModel> GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<RatingViewModel> UpdateRating(UpdateRatingRequestModel ratingUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
