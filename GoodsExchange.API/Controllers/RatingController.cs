using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/ratings")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _ratingService.GetRatingById(id);
            if (!result.IsSuccessed)
            {
                return NotFound(result.Message);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = SystemConstant.Roles.Customer)]
        public async Task<IActionResult> GetAll([FromQuery]PagingRequestModel paging, [FromQuery]RatingsRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ratingService.GetRatings(paging, request);
            return Ok(result);
        }

        [HttpPost]
        [Route("send-rating")]
        [Authorize(Roles = SystemConstant.Roles.Customer)]
        public async Task<IActionResult> SendRating([FromBody] CreateRatingRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ratingService.SendRating(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
