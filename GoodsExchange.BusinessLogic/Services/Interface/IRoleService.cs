using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.ViewModels.Role;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IRoleService
    {
        Task<Guid> GetRoleIdOfRoleName(string roleName);
        Task<ResponseModel<List<RoleViewModel>>> GetAllRole();

    }
}
