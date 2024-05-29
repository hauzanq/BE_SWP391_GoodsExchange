using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _userService.Authenticate(request);
            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateUser(id,request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Route("update/active")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        public async Task<IActionResult> ActiveUser(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.ActiveUser(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Route("update/deactive")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        public async Task<IActionResult> DeActiveUser(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.DeActiveUser(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        [Authorize(Roles = SystemConstant.Roles.Administrator)]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequestModel paging, [FromQuery] SearchRequestModel search, [FromQuery] GetAllUserRequestModel model)
        {
            var result = await _userService.GetAll(paging, search, model);
            return Ok(result);
        }
    }
}
