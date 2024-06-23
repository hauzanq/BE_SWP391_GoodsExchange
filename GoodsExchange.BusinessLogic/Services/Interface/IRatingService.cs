using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.ViewModels.Rating;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IRatingService
    {
        Task<int> CountNumberRatingOfUser(Guid id);
        Task<float> CountAverageNumberStarsOfUser(Guid id);
        Task<EntityResponse<RatingViewModel>> SendRating(CreateRatingRequestModel request);
        Task<PageResult<RatingViewModel>> GetRatings(PagingRequestModel paging, RatingsRequestModel request);
        Task<EntityResponse<RatingViewModel>> GetRatingById(Guid id);
    }
}
