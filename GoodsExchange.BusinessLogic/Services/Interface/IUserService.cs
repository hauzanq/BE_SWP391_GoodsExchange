using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels.User;

namespace GoodsExchange.BusinessLogic.Services.Interface
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
}
