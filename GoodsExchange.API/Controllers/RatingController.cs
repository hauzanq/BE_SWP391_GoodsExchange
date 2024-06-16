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

        [HttpPost]
        [Authorize(Roles = SystemConstant.Roles.Buyer)]
        public async Task<IActionResult> SendRating(CreateRatingRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ratingService.SendRating(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);  
        }

        [HttpGet]
        [Authorize(Roles = SystemConstant.Roles.Seller)]
        public async Task<IActionResult> GetAll([FromQuery]PagingRequestModel paging, [FromQuery]RatingsRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ratingService.GetAll(paging, request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = SystemConstant.Roles.Seller)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ratingService.GetById(id);
            if (!result.IsSuccessed)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
