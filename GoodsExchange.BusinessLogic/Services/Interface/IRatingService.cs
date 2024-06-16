using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.ViewModels.Rating;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IRatingService
    {
        Task<int> CountNumberRatingOfUser(Guid id);
        Task<float> CountAverageNumberStarsOfUser(Guid id);
        Task<ApiResult<RatingViewModel>> SendRating(CreateRatingRequestModel request);
        Task<ApiResult<RatingViewModel>> GetById(Guid id);
        Task<PageResult<RatingViewModel>> GetAll(PagingRequestModel paging, RatingsRequestModel request);
    }
}
