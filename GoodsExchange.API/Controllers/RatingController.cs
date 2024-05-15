using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.Services;
using GoodsExchange.BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/ratings")]
    public class RatingController : ControllerBase
    {

        private IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public ActionResult<RatingViewModel> CreateRating(CreateRatingRequestModel ratingCreate)
        {
            var ratingCreated = _ratingService.CreateRating(ratingCreate);

            if (ratingCreated == null)
            {
                return NotFound("");
            }
            return ratingCreated;
        }


        [HttpGet]
        public ActionResult<List<RatingViewModel>> GetAll()
        {
            var ratingList = _ratingService.GetAll();

            if (ratingList == null)
            {
                return NotFound("");
            }
            return ratingList;
        }


        [HttpGet("idTmp")]
        public ActionResult<RatingViewModel> GetById(int idTmp)
        {
            var ratingDetail = _ratingService.GetById(idTmp);

            if (ratingDetail == null)
            {
                return NotFound("");
            }
            return ratingDetail;
        }


        [HttpDelete]
        public ActionResult<bool> DeleteRating(int idTmp)
        {
            var check = _ratingService.DeleteRating(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }


        [HttpPut]
        public ActionResult<RatingViewModel> UpdateRating(UpdateRatingRequestModel ratingCreate)
        {
            var ratingUpdated = _ratingService.UpdateRating(ratingCreate);

            if (ratingUpdated == null)
            {
                return NotFound("");
            }
            return ratingUpdated;
        }
    }

}
