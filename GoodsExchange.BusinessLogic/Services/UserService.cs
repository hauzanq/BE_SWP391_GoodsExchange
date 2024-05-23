using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<UserViewModel>> CreateUser(RegisterRequest userCreate);
        Task<ApiResult<UserViewModel>> UpdateUser(UpdateUserRequestModel userUpdate);
        Task<ApiResult<bool>> DeleteUser(Guid id);
        Task<ApiResult<List<UserViewModel>>> GetAll();
        Task<ApiResult<UserViewModel>> GetById(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        public UserService(GoodsExchangeDbContext context, IRoleService roleService, IConfiguration configuration)
        {
            _context = context;
            _roleService = roleService;
            _configuration = configuration;
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _context.Users.Where(u => u.UserName == request.UserName && u.Password == request.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiErrorResult<string>("User does not exist");
            }

            var roles = (await _roleService.GetUserRole(user.UserId)).Data;

            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, string.Join(";",roles))
            };

            // Đóng gói secret key vào SymmetricSecurityKey để ký và giải mã token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Issuer"],
                    claims: userClaims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds
                );

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public Task<ApiResult<UserViewModel>> CreateUser(RegisterRequest userCreate)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<List<UserViewModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<UserViewModel>> UpdateUser(UpdateUserRequestModel userUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
