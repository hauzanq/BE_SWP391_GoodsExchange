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
            
        //Task<ResponseModel<UserProfileViewModel>> UpdateUserAsync(UpdateUserRequestModel request);

        Task<ResponseModel<UpdateProfileUserRequestModel>> UpdateUserForCustomerAsync(UpdateProfileUserRequestModel request);
        Task<ResponseModel<bool>> ChangeUserStatusAsync(Guid id, bool status);
        Task<ResponseModel<bool>> ChangeUserRoleAndStatusAsync(UpdateUserRoleRequestModel request);
        Task<ResponseModel<PageResult<AdminUserViewModel>>> GetUsers(PagingRequestModel paging, string keyword, GetUserRequestModel model);
        Task<ResponseModel<UserProfileViewModel>> GetUserByIdAsync(Guid id);
        Task<ResponseModel<string>> ChangePasswordAsync(ChangePasswordRequestModel request);
        Task<ResponseModel<string>> ForgotPasswordAsync(ChangePasswordRequestModel request);
        Task<User> GetUserByProductId(Guid id);
        Task<User> GetUserAsync(Guid id);
        Task<string> GetUserFullNameAsync(Guid id);
    }
}
