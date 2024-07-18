using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
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
                throw new NotFoundException("User does not exist.");
            }
            return user;
        }
        public async Task<ResponseModel<bool>> ChangeUserStatusAsync(Guid id, bool status)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist.");
            }

            user.IsActive = status;
            await _context.SaveChangesAsync();

            return new ResponseModel<bool>("User status changed successfully.");
        }
        public async Task<ResponseModel<bool>> ChangeUserRoleAndStatusAsync(UpdateUserRoleRequestModel request)
        {
            var user = await _context.Users.FindAsync(request.id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist.");
            }

            user.IsActive = request.status;
            //var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.roleId);
            //if (role == null)
            //{
            //    throw new NotFoundException("Role not found.");
            //}
            //user.RoleId = role.RoleId;
            await _context.SaveChangesAsync();

            return new ResponseModel<bool>(true);
        }

        public async Task<ResponseModel<LoginViewModel>> Login(LoginRequestModel request)
        {
            var user = await _context.Users
                                    .Include(u => u.Role)
                                    .Where(u => u.UserName == request.UserName && u.Password == request.Password)
                                    .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException("User does not exist.");
            }

            if (!user.IsActive)
            {
                throw new BadRequestException("User account is inactive");
            }

            if (!user.EmailConfirm)
            {
                throw new BadRequestException("The emails doesn't vertified , Please check yours gmail : " + user.Email + "to vertified account !!");
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

            return new ResponseModel<LoginViewModel>("Login successful.", new LoginViewModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = user.Role.RoleName
            });
        }

        public async Task<ResponseModel<string>> ChangePasswordAsync(ChangePasswordRequestModel request)
        {
            var user = await _context.Users.FindAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            //if (request.UserName != user.UserName)
            //{
            //    throw new BadRequestException("UserName is incorrect.");
            //}

            if (request.OldPassword != user.Password)
            {
                throw new BadRequestException("OldPassword is incorrect.");
            }

            if (request.ConfirmNewPassword != request.ConfirmNewPassword)
            {
                throw new BadRequestException("New password and confirm new password do not match.");
            }

            user.Password = request.NewPassword;
            await _context.SaveChangesAsync();

            return new ResponseModel<string>("Change password successfully.");

        }

        public Task<ResponseModel<string>> ForgotPasswordAsync(ChangePasswordRequestModel request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<PageResult<AdminUserViewModel>>> GetUsers(PagingRequestModel paging, string keyword, GetUserRequestModel model)
        {
            var query = _context.Users
                                .Include(u => u.Role)
                                .Where(u => u.Role.RoleName == SystemConstant.Roles.Moderator)
                                .AsQueryable();

            #region Searching
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(u => u.FirstName.Contains(keyword)
                                        || u.LastName.Contains(keyword)
                                        || u.Email.Contains(keyword));
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
                UserId = u.UserId,
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

            return new ResponseModel<PageResult<AdminUserViewModel>>(result);
        }

        public async Task<ResponseModel<UserProfileViewModel>> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User does not exist.");
            }

            var result = new UserProfileViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                UserImageUrl = user.UserImageUrl
            };
            return new ResponseModel<UserProfileViewModel>("User profile retrieved successfully.", result);
        }

        public async Task<ResponseModel<UserProfileViewModel>> Register(RegisterRequestModel request)
        {
            var usernameAvailable = await _context.Users.AnyAsync(u => u.UserName == request.UserName);
            if (usernameAvailable)
            {
                throw new BadRequestException("Username available.");
            }

            var emailAvailable = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (emailAvailable)
            {
                throw new BadRequestException("Email available.");
            }

            if (request.Password != request.ConfirmPassword)
            {
                throw new BadRequestException("The confirm password is incorrect.");
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

            return new ResponseModel<UserProfileViewModel>("Registration successful.", result);
        }

        public async Task<ResponseModel<UserProfileViewModel>> CreateAccountByAdmin(RegisterRequestModel request)
        {
            var usernameAvailable = await _context.Users.AnyAsync(u => u.UserName == request.UserName);
            if (usernameAvailable)
            {
                throw new BadRequestException("Username available.");
            }

            var emailAvailable = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (emailAvailable)
            {
                throw new BadRequestException("Email available.");
            }

            if (request.Password != request.ConfirmPassword)
            {
                throw new BadRequestException("The confirm password is incorrect.");
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
                EmailConfirm = true,
                RoleId = await _serviceWrapper.RoleServices.GetRoleIdOfRoleName(SystemConstant.Roles.Moderator)
            };


            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = new UserProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                UserImageUrl = user.UserImageUrl,
                UserName = user.UserName,
            };

            return new ResponseModel<UserProfileViewModel>("Create Account with role Moderator Successfully",result);
        }

        public async Task<ResponseModel<UserProfileViewModel>> UpdateUserAsync(UpdateUserRequestModel request)
        {
            var user = await _context.Users.FindAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            var usernameAvailable = await _context.Users.AnyAsync(u => u.UserName == request.UserName && u.UserId != user.UserId);
            if (usernameAvailable)
            {
                throw new BadRequestException("Username available.");
            }

            var emailAvailable = await _context.Users.AnyAsync(u => u.Email == request.Email && u.UserId != user.UserId);
            if (emailAvailable)
            {
                throw new BadRequestException("Email available.");
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

            return new ResponseModel<UserProfileViewModel>("User profile updated successfully.");
        }


        public async Task<ResponseModel<UpdateProfileUserRequestModel>> UpdateUserForCustomerAsync(UpdateProfileUserRequestModel request)
        {
            var user = await _context.Users.FindAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            //var usernameAvailable = await _context.Users.AnyAsync(u => u.UserName == request.userName && u.UserId != user.UserId);
            //if (usernameAvailable)
            //{
            //    throw new BadRequestException("Username available.");
            //}

            var emailAvailable = await _context.Users.AnyAsync(u => u.Email == request.Email && u.UserId != user.UserId);
            if (emailAvailable)
            {
                throw new BadRequestException("Email available.");
            }
           
            
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
       
            // If Email update doesn't exist when update then rwquire comfirm email then update
            if(user.Email != request.Email)
            {
                user.Email = request.Email;
                var token = GenerateEmailVerificationToken(user.Email);
                user.EmailConfirm = false;
                await _emailService.SendEmailToUpdateProfile(user.Email, token);

            }
            user.DateOfBirth = request.DateOfBirth;
            user.PhoneNumber = request.PhoneNumber;
            
            //user.UserName = request.userName;
            await _context.SaveChangesAsync();

            return new ResponseModel<UpdateProfileUserRequestModel>("User profile updated successfully.");
        }


        public async Task<ResponseModel<UserProfileViewModel>> UpdateUserByAdminAsync(UpdateUserRequestModel request)
        {
            var user = await _context.Users.FindAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));
            var usernameAvailable = await _context.Users.AnyAsync(u => u.UserName == request.UserName && u.UserId != user.UserId);
            if (usernameAvailable)
            {
                throw new BadRequestException("Username available.");
            }

            var emailAvailable = await _context.Users.AnyAsync(u => u.Email == request.Email && u.UserId != user.UserId);
            if (emailAvailable)
            {
                throw new BadRequestException("Email available.");
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

            return new ResponseModel<UserProfileViewModel>(result);
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
                throw new NotFoundException("User does not exist.");
            }
            return user.FirstName + " " + user.LastName;
        }

        public async Task<User> GetUserByProductId(Guid id)
        {
            var product = await _serviceWrapper.ProductServices.GetProductAsync(id);
            if (product == null)
            {
                throw new NotFoundException("User does not exist.");
            }
            return await _serviceWrapper.UserServices.GetUserAsync(product.UserUploadId);
        }

      
    
    }
}
