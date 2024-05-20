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
        public async Task<ActionResult<RatingViewModel>> CreateRating(CreateRatingRequestModel ratingCreate)
        {
            var ratingCreated = await _ratingService.CreateRating(ratingCreate);

            if (ratingCreated == null)
            {
                return NotFound("");
            }
            return ratingCreated;
        }


        [HttpGet]
        public async Task<ActionResult<List<RatingViewModel>>> GetAll()
        {
            var ratingList = await _ratingService.GetAll();

            if (ratingList == null)
            {
                return NotFound("");
            }
            return ratingList;
        }


        [HttpGet("idTmp")]
        public async Task<ActionResult<RatingViewModel>> GetById(int idTmp)
        {
            var ratingDetail = await _ratingService.GetById(idTmp);

            if (ratingDetail == null)
            {
                return NotFound("");
            }
            return ratingDetail;
        }


        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteRating(int idTmp)
        {
            var check = await _ratingService.DeleteRating(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }


        [HttpPut]
        public async Task<ActionResult<RatingViewModel>> UpdateRating(UpdateRatingRequestModel ratingCreate)
        {
            var ratingUpdated = await _ratingService.UpdateRating(ratingCreate);

            if (ratingUpdated == null)
            {
                return NotFound("");
            }
            return ratingUpdated;
        }
    }

}
