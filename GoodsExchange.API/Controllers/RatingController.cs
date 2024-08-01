using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Rating;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/ratings")]
    public class RatingController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;

        public RatingController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<RatingViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceWrapper.RatingServices.GetRatingById(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        [ProducesResponseType(typeof(PageResult<RatingViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequestModel paging, [FromQuery] RatingsRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.RatingServices.GetRatings(paging, request);
            return Ok(result);
        }

        [HttpGet]
        [Route("user/{userid}")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(PageResult<RatingViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRatingsForUser(Guid userid, [FromQuery] PagingRequestModel paging, [FromQuery] RatingsRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.RatingServices.GetRatings(paging, request, userid);
            return Ok(result);
        }

        [HttpPost]
        [Route("send-rating")]
        [Authorize(Roles = SystemConstant.Roles.User)]
        [ProducesResponseType(typeof(ResponseModel<RatingViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendRating([FromBody] CreateRatingRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.RatingServices.SendRating(request);
            return Ok(result);
        }
    }
}
