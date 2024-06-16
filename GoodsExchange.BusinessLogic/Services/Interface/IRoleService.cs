using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Role;
using GoodsExchange.BusinessLogic.ViewModels.Role;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IRoleService
    {
        Task<ApiResult<RoleViewModel>> CreateRole(CreateRoleRequestModel request);
        Task<ApiResult<RoleViewModel>> UpdateRole(UpdateRoleRequestModel request);
        Task<ApiResult<bool>> DeleteRole(Guid id);
        Task<ApiResult<List<RoleViewModel>>> GetAll();
        Task<ApiResult<RoleViewModel>> GetById(Guid id);
        Task<List<string>> GetRolesOfUser(Guid id);
        Task<Guid> GetRoleIdOfRoleName(string roleName);
    }
}
