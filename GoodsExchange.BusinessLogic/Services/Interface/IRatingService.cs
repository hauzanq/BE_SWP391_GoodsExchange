using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.ViewModels.Rating;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IRatingService
    {
        Task<int> CountNumberRatingOfUser(Guid id);
        Task<float> CountAverageNumberStarsOfUser(Guid id);
        Task<ResponseModel<RatingViewModel>> SendRating(CreateRatingRequestModel request);
        Task<ResponseModel<PageResult<RatingViewModel>>> GetRatings(PagingRequestModel paging, RatingsRequestModel request, Guid? userid = null);
        Task<ResponseModel<RatingViewModel>> GetRatingById(Guid id);
        Task<bool> IsUserRatedOnProduct(Guid userid, Guid productid);
    }
}
