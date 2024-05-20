using GoodsExchange.BusinessLogic.RequestModels.Role;
using GoodsExchange.BusinessLogic.ViewModels;

namespace GoodsExchange.BusinessLogic.Services
{
    public interface IRoleService
    {
        Task<RoleViewModel> CreateRole(CreateRoleRequestModel roleCreate);
        Task<RoleViewModel> UpdateRole(UpdateRoleRequestModel roleUpdate);
        Task<bool> DeleteRole(int idTmp);
        Task<List<RoleViewModel>> GetAll();
        Task<RoleViewModel> GetById(int idTmp);
    }

    public class RoleService : IRoleService
    {
        public Task<RoleViewModel> CreateRole(CreateRoleRequestModel roleCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRole(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoleViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<RoleViewModel> GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<RoleViewModel> UpdateRole(UpdateRoleRequestModel roleUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
