using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels.User;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IUserService
    {
        Task<EntityResponse<LoginViewModel>> Login(LoginRequestModel request);
        Task<EntityResponse<UserProfileViewModel>> Register(RegisterRequestModel request);
        Task<EntityResponse<UserProfileViewModel>> CreateAccountByAdmin(RegisterRequestModel request);

        Task<EntityResponse<UserProfileViewModel>> UpdateUserAsync(UpdateUserRequestModel request);
        Task<EntityResponse<bool>> ChangeUserStatusAsync(Guid id, bool status);
        Task<EntityResponse<bool>> ChangeUserRoleAndStatusAsync(UpdateUserRoleRequestModel request);
        Task<PageResult<AdminUserViewModel>> GetUsers(PagingRequestModel paging, string keyword, GetUserRequestModel model);
        Task<EntityResponse<UserProfileViewModel>> GetUserByIdAsync(Guid id);
        Task<EntityResponse<string>> ChangePasswordAsync(ChangePasswordRequestModel request);
        Task<EntityResponse<string>> ForgotPasswordAsync(ChangePasswordRequestModel request);
        Task<User> GetUserByProductId(Guid id);
        Task<User> GetUserAsync(Guid id);
        Task<string> GetUserFullNameAsync(Guid id);
    }
}
