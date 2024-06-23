using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.Services.Interface;
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

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IEmailService _emailService;
        public UserService(GoodsExchangeDbContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IServiceWrapper serviceWrapper, IEmailService emailService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _serviceWrapper = serviceWrapper;
            _emailService = emailService;
        }
        public async Task<User> GetUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<ApiResult<bool>> ChangeUserStatusAsync(Guid id, bool status)
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
            var user = await _context.Users
                                    .Include(u=>u.Role)
                                    .Where(u => u.UserName == request.UserName && u.Password == request.Password)
                                    .FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiErrorResult<LoginViewModel>("User does not exist");
            }

            if (!user.IsActive)
            {
                return new ApiErrorResult<LoginViewModel>("User account is inactive");
            }
            if (!user.EmailConfirm)
            {
                return new ApiErrorResult<LoginViewModel>("The emails doesn't vertified , Please check yours gmail : " + user.Email + "to vertified account !!");
            }

            var userClaims = new[]
            {
                new Claim("id",user.UserId.ToString()),
                new Claim("username",user.UserName),
                new Claim("emailaddress", user.Email),
                new Claim("mobilephone", user.PhoneNumber),
                new Claim("roles", user.Role.RoleName)
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
                Role = user.Role.RoleName
            });
        }

        public async Task<ApiResult<string>> ChangePasswordAsync(ChangePasswordRequestModel request)
        {
            var user = await _context.Users.FindAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            if (request.UserName != user.UserName)
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

        public Task<ApiResult<string>> ForgotPasswordAsync(ChangePasswordRequestModel request)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResult<AdminUserViewModel>> GetAllUsersAsync(PagingRequestModel paging, SearchRequestModel search, GetUserRequestModel model)
        {
            var query = _context.Users
                                .Include(u => u.Role)
                                .Where(u => u.Role.RoleName == SystemConstant.Roles.Moderator)
                                .AsQueryable();

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
                query = query.Where(u => u.Role.RoleName.Contains(model.RoleName));
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
                RoleName = u.Role.RoleName,
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

        public async Task<ApiResult<UserProfileViewModel>> GetUserByIdAsync(Guid id)
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

            if (request.Password != request.ConfirmPassword)
            {
                return new ApiErrorResult<UserProfileViewModel>("The confirm password is incorrect.");
            }

            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                UserImageUrl = SystemConstant.Images.UserImageDefault,
                UserName = request.UserName,
                Password = request.Password,
                IsActive = true,
                RoleId = await _serviceWrapper.RoleServices.GetRoleIdOfRoleName(SystemConstant.Roles.Customer)
            };

            var token = GenerateEmailVerificationToken(user.Email);

            await _emailService.SendEmailToRegisterAsync(user.Email, token);
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

        public async Task<ApiResult<UserProfileViewModel>> UpdateUserAsync(UpdateUserRequestModel request)
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
            user.UserImageUrl = await _serviceWrapper.FirebaseStorageServices.UploadUserImage(request.FirstName + " " + request.LastName, request.Image);
            user.UserName = request.UserName;
            user.Password = request.Password;

            await _context.SaveChangesAsync();

            return new ApiSuccessResult<UserProfileViewModel>();
        }

        public string GenerateEmailVerificationToken(string email)
        {
            var key = _configuration["JWTAuthentication:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("JWTAuthentication:Key is not configured.");
            }

            var keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length < 32)
            {
                throw new ArgumentException("The key size must be at least 256 bits (32 bytes). Please use a longer key.");
            }

            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(3),
                Issuer = _configuration["JWTAuthentication:Issuer"],
                Audience = _configuration["JWTAuthentication:Audience"],
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GetUserFullNameAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return "";
            }
            return user.FirstName + " " + user.LastName;
        }

        public async Task<User> GetUserByProductId(Guid id)
        {
            var product = await _serviceWrapper.ProductServices.GetProductAsync(id);
            if (product == null)
            {
                return null;
            }
            return await _serviceWrapper.UserServices.GetUserAsync(product.UserUploadId);
        }
    }
}
