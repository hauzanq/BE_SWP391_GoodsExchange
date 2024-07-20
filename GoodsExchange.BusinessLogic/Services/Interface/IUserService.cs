using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels.User;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IUserService
    {
        Task<ResponseModel<LoginViewModel>> Login(LoginRequestModel request);
        Task<ResponseModel<UserProfileViewModel>> Register(RegisterRequestModel request);
        Task<ResponseModel<UserProfileViewModel>> CreateAccountByAdmin(RegisterRequestModel request);
        Task<ResponseModel<UserProfileViewModel>> UpdateUserAsync(UpdateUserRequestModel request);
        Task<ResponseModel<bool>> ChangeUserStatusAsync(Guid id, bool status);
        Task<ResponseModel<PageResult<AdminUserViewModel>>> GetUsersAsync(PagingRequestModel paging, string keyword, UsersRequestModel model);
        Task<ResponseModel<UserProfileViewModel>> GetUserByIdAsync(Guid id);
        Task<ResponseModel<string>> ChangePasswordAsync(ChangePasswordRequestModel request);
        Task<User> GetUserByProductId(Guid id);
        Task<User> GetUserAsync(Guid id);
        Task<string> GetUserFullNameAsync(Guid id);
    }
}
