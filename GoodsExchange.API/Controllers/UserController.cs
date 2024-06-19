using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.Services;
using GoodsExchange.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GoodsExchange.BusinessLogic.Services.Emails;
using System.Net.WebSockets;
using GoodsExchange.Data.Models;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly GoodsExchangeDbContext _context;
        private readonly IEmailServices _emailServices;


        public UserController(IUserService userService, IConfiguration configuration, GoodsExchangeDbContext context, IEmailServices emailServices)
        {
            _userService = userService;
            _configuration = configuration;
            _context = context;
            _emailServices = emailServices;
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
        public async Task<IActionResult> Register(RegisterRequestModel request)
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
        public async Task<IActionResult> UpdateUser(UpdateUserRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateUser(request);
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

            var result = await _userService.ChangeUserStatus(id, status);
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
            var result = await _userService.GetAll(paging, search, model);
            return Ok(result);
        }

        [HttpGet]
        [Route("verifyemail")]
        public IActionResult VerifyEmail(string email,string token)
        {
            var Usertoken = _context.UserTokens.FirstOrDefault(u => u.Token.Equals(token));
            if (Usertoken == null) {
                return BadRequest("Token doesn't exist");
            }
            var user = _context.Users.FirstOrDefault(u=> u.Email == email);
            if (user == null)
            {
                return BadRequest("User doesn't exist");
            }
            user.EmailConfirm = true;
            _context.Remove(Usertoken); // Optionally, remove the token after it's used.
            _context.Update(user);
            _context.SaveChanges();

            return Ok(" vertified Email successfully , Welcome to FGoodExchangeFU of us . ");

               
        }

        [HttpGet]
        [Route("Reset-passsword")]
        public IActionResult ResetPassword(string email)
        {
            using(var context = new GoodsExchangeDbContext()) { 
            
                var existedUser = context.Users.SingleOrDefault(x => x.Email == email);
                if(existedUser == null)
                {
                    return BadRequest("User doesn't existed");
                }
                 
                return null;
            }
        }


     







    }
}
