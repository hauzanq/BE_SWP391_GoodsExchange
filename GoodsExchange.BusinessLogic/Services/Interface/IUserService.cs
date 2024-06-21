using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels.User;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResult<LoginViewModel>> Login(LoginRequestModel request);
        Task<ApiResult<UserProfileViewModel>> Register(RegisterRequestModel request);
        Task<ApiResult<UserProfileViewModel>> UpdateUserAsync(UpdateUserRequestModel request);
        Task<ApiResult<bool>> ChangeUserStatusAsync(Guid id, bool status);
        Task<PageResult<AdminUserViewModel>> GetAllUsersAsync(PagingRequestModel paging, SearchRequestModel search, GetUserRequestModel model);
        Task<ApiResult<UserProfileViewModel>> GetUserByIdAsync(Guid id);
        Task<ApiResult<string>> ChangePasswordAsync(ChangePasswordRequestModel request);
        Task<ApiResult<string>> ForgotPasswordAsync(ChangePasswordRequestModel request);
        Task<User> GetUserByProductId(Guid id);
        Task<User> GetUserAsync(Guid id);
        Task<string> GetUserFullNameAsync(Guid id);
    }
}
