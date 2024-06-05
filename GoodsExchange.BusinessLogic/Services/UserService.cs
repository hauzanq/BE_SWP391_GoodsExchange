using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels.User;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.AspNetCore.Http;
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
        Task<ApiResult<LoginViewModel>> Login(LoginRequestModel request);
        Task<ApiResult<UserProfileViewModel>> Register(RegisterRequestModel request);
        Task<ApiResult<UserProfileViewModel>> UpdateUser(UpdateUserRequestModel request);
        Task<ApiResult<bool>> ChangeUserStatus(Guid id, bool status);
        Task<PageResult<AdminUserViewModel>> GetAll(PagingRequestModel paging, SearchRequestModel search, GetUserRequestModel model);
        Task<ApiResult<UserProfileViewModel>> GetById(Guid id);
        Task<ApiResult<string>> ChangePassword(ChangePasswordRequestModel request);
        Task<ApiResult<string>> ForgotPassword(ChangePasswordRequestModel request);
    }

    public class UserService : IUserService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        public UserService(GoodsExchangeDbContext context, IRoleService roleService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _roleService = roleService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<bool>> ChangeUserStatus(Guid id, bool status)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ApiErrorResult<bool>("User does not exist");
            }

            user.IsActive = status;
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<LoginViewModel>> Login(LoginRequestModel request)
        {
            var user = await _context.Users.Where(u => u.UserName == request.UserName && u.Password == request.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiErrorResult<LoginViewModel>("User does not exist");
            }

            if (!user.IsActive)
            {
                return new ApiErrorResult<LoginViewModel>("User account is inactive");
            }

            var roles = await _roleService.GetRolesOfUser(user.UserId);

            var userClaims = new[]
            {
                new Claim("id",user.UserId.ToString()),
                new Claim("username",user.UserName),
                new Claim("emailaddress", user.Email),
                new Claim("mobilephone", user.PhoneNumber),
                new Claim("roles", string.Join(";",roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTAuthentication:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: _configuration["JWTAuthentication:Issuer"],
                    audience: _configuration["JWTAuthentication:Issuer"],
                    claims: userClaims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds
                );

            return new ApiSuccessResult<LoginViewModel>(new LoginViewModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Roles = _roleService.GetRolesOfUser(user.UserId).Result
            });
        }

        public async Task<ApiResult<string>> ChangePassword(ChangePasswordRequestModel request)
        {
            var user = await _context.Users.FindAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            if (request.UserName != user.UserName )
            {
                return new ApiErrorResult<string>("UserName is incorrect.");
            }

            if (request.OldPassword != user.Password)
            {
                return new ApiErrorResult<string>("OldPassword is incorrect.");
            }

            if (request.ConfirmNewPassword != request.ConfirmNewPassword)
            {
                return new ApiErrorResult<string>("New password and confirm new password do not match.");
            }

            user.Password = request.NewPassword;
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<string>("Change password successfully.");

        }
        
        public Task<ApiResult<string>> ForgotPassword(ChangePasswordRequestModel request)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResult<AdminUserViewModel>> GetAll(PagingRequestModel paging, SearchRequestModel search, GetUserRequestModel model)
        {
            var query = _context.Users.Include(u => u.UserRoles).AsQueryable().Where(u=>u.UserRoles.Any(ur=>ur.Role.RoleName == SystemConstant.Roles.Moderator));

            #region Searching
            if (!string.IsNullOrEmpty(search.KeyWords))
            {
                query = query.Where(u => u.FirstName.Contains(search.KeyWords)
                                        || u.LastName.Contains(search.KeyWords)
                                        || u.Email.Contains(search.KeyWords));
            }
            #endregion

            #region Filtering
            if (!string.IsNullOrEmpty(model.RoleName))
            {
                query = query.Where(u => u.UserRoles.Any(ur => ur.Role.RoleName.Contains(model.RoleName)));
            }

            if (model.Status != null)
            {
                query = query.Where(u => u.IsActive == model.Status);
            }

            #endregion

            #region Sorting
            if (!string.IsNullOrEmpty(model.FirstName) && model.FirstName.ToLower() == "asc")
            {
                query = query.OrderBy(p => p.FirstName);
            }
            else if (!string.IsNullOrEmpty(model.FirstName) && model.FirstName.ToLower() == "desc")
            {
                query = query.OrderByDescending(p => p.FirstName);
            }

            if (!string.IsNullOrEmpty(model.LastName) && model.LastName.ToLower() == "asc")
            {
                query = query.OrderBy(p => p.LastName);
            }
            else if (!string.IsNullOrEmpty(model.LastName) && model.LastName.ToLower() == "desc")
            {
                query = query.OrderByDescending(p => p.LastName);
            }

            if (!string.IsNullOrEmpty(model.Email) && model.Email.ToLower() == "asc")
            {
                query = query.OrderBy(p => p.Email);
            }
            else if (!string.IsNullOrEmpty(model.Email) && model.Email.ToLower() == "desc")
            {
                query = query.OrderByDescending(p => p.Email);
            }
            #endregion

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            var data = await query.Select(u => new AdminUserViewModel()
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                RoleName = string.Join(", ", u.UserRoles.Where(ur => ur.UserId == u.UserId).Select(ur => ur.Role.RoleName)),
                Status = u.IsActive
            }).ToListAsync();

            var result = new PageResult<AdminUserViewModel>()
            {
                Items = data,
                TotalPage = totalPages,
                CurrentPage = paging.PageIndex
            };

            return result;
        }

        public async Task<ApiResult<UserProfileViewModel>> GetById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ApiErrorResult<UserProfileViewModel>("User does not exist");
            }

            var result = new UserProfileViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                UserImageUrl = user.UserImageUrl
            };
            return new ApiSuccessResult<UserProfileViewModel>(result);
        }

        public async Task<ApiResult<UserProfileViewModel>> Register(RegisterRequestModel request)
        {
            var usernameAvailable = await _context.Users.AnyAsync(u => u.UserName == request.UserName);
            if (usernameAvailable)
            {
                return new ApiErrorResult<UserProfileViewModel>("Username available.");
            }

            var emailAvailable = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (emailAvailable)
            {
                return new ApiErrorResult<UserProfileViewModel>("Email available.");
            }

            var buyer = await _roleService.GetRoleIdOfRoleName(SystemConstant.Roles.Buyer);
            var seller = await _roleService.GetRoleIdOfRoleName(SystemConstant.Roles.Seller);

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                UserImageUrl = request.UserImageUrl,
                UserName = request.UserName,
                Password = request.Password,
                IsActive = true,
                UserRoles = new List<UserRole>
                {
                    new UserRole { RoleId = await _roleService.GetRoleIdOfRoleName(SystemConstant.Roles.Buyer) },
                    new UserRole { RoleId = await _roleService.GetRoleIdOfRoleName(SystemConstant.Roles.Seller) }
                }
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = new UserProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                UserImageUrl = user.UserImageUrl,
                UserName = user.UserName,
            };

            return new ApiSuccessResult<UserProfileViewModel>(result);
        }

        public async Task<ApiResult<UserProfileViewModel>> UpdateUser(UpdateUserRequestModel request)
        {
            var user = await _context.Users.FindAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            var usernameAvailable = await _context.Users.AnyAsync(u => u.UserName == request.UserName && u.UserId != user.UserId);
            if (usernameAvailable)
            {
                return new ApiErrorResult<UserProfileViewModel>("Username available.");
            }

            var emailAvailable = await _context.Users.AnyAsync(u => u.Email == request.Email && u.UserId != user.UserId);
            if (emailAvailable)
            {
                return new ApiErrorResult<UserProfileViewModel>("Email available.");
            }

            
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.DateOfBirth = request.DateOfBirth;
            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.UserName;
            user.Password = request.Password;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<UserProfileViewModel>();
        }
    }
}
