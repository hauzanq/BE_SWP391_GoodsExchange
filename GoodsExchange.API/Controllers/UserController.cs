using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _userService.Login(request);

            return Ok(token);
        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequestModel request)
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
        [Route("update-account")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateUserAsync(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch]
        [Route("status/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Moderator)]
        public async Task<IActionResult> ChangeUserStatus(Guid id, bool status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.ChangeUserStatusAsync(id, status);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("list-moderators")]
        [Authorize(Roles = SystemConstant.Roles.Administrator)]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequestModel paging, [FromQuery] SearchRequestModel search, [FromQuery] GetUserRequestModel model)
        {
            var result = await _userService.GetAllUsersAsync(paging, search, model);
            return Ok(result);
        }

        [HttpGet]
        [Route("verifyemail")]
        public IActionResult VerifyEmail(string email, string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWTAuthentication:Key"]);
            var securityKey = new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false, 
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);

                var tokenEmail = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (tokenEmail != email)
                {
                    return BadRequest("Invalid token.");
                }

                using (var context = new GoodsExchangeDbContext())
                {
                    var user = context.Users.SingleOrDefault(u => u.Email == email);

                    if (user == null)
                    {
                        return BadRequest("User not found.");
                    }

                    user.EmailConfirm = true;
                    context.SaveChanges();
                    return Ok("Email verified successfully.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Invalid or expired token.");
            }
        }








    }
}
