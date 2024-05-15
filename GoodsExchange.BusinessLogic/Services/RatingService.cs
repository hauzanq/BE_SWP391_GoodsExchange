using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.ViewModels;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IRatingService
    {
        public RatingViewModel CreateRating(CreateRatingRequestModel ratingCreate);
        public RatingViewModel UpdateRating(UpdateRatingRequestModel ratingUpdate);
        public bool DeleteRating(int idTmp);
        public List<RatingViewModel> GetAll();
        public RatingViewModel GetById(int idTmp);
    }

    public class RatingService : IRatingService
    {

        public RatingService()
        {
            
        }
        public RatingViewModel CreateRating(CreateRatingRequestModel ratingCreate)
        {
            throw new NotImplementedException();
        }

        public RatingViewModel UpdateRating(UpdateRatingRequestModel ratingUpdate)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRating(int idTmp)
        {
            throw new NotImplementedException();
        }

        public List<RatingViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public RatingViewModel GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

    }

}
