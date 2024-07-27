using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.User;
using GoodsExchange.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IConfiguration _configuration;
        public UserController(IServiceWrapper serviceWrapper, IConfiguration configuration)
        {
            _serviceWrapper = serviceWrapper;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<LoginViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceWrapper.UserServices.Login(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<UserProfileViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromForm] RegisterRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceWrapper.UserServices.Register(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateAccount")]
        [Authorize(Roles = SystemConstant.Roles.Administrator)]
        [ProducesResponseType(typeof(ResponseModel<UserProfileViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAccountByAdmin([FromForm] RegisterRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceWrapper.UserServices.CreateAccountByAdmin(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<UserProfileViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _serviceWrapper.UserServices.GetUserByIdAsync(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("update-account")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseModel<UserProfileViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceWrapper.UserServices.UpdateUserAsync(request);
            return Ok(result);
        }

        [HttpPatch]
        [Route("changing-password")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseModel<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel request)
        {
            var result = await _serviceWrapper.UserServices.ChangePasswordAsync(request);
            return Ok(result);
        }

        [HttpPatch]
        [Route("status/{id}")]
        [Authorize(Roles = SystemConstant.Roles.Administrator)]
        [ProducesResponseType(typeof(ResponseModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeUserStatus(Guid id, bool status)
        {
            var result = await _serviceWrapper.UserServices.ChangeUserStatusAsync(id, status);
            return Ok(result);
        }

        [HttpPost]
        [Route("list-users")]
        [Authorize(Roles = SystemConstant.Roles.Administrator)]
        [ProducesResponseType(typeof(PageResult<AdminUserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequestModel paging, [FromQuery] string? keyword, [FromQuery] UsersRequestModel model)
        {
            var result = await _serviceWrapper.UserServices.GetUsersAsync(paging, keyword, model);
            return Ok(result);
        }

        [HttpGet]
        [Route("verify-email")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
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
