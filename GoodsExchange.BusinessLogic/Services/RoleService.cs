using GoodsExchange.BusinessLogic.RequestModels.Role;
using GoodsExchange.BusinessLogic.ViewModels;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IRoleService
    {
        public RoleViewModel CreateRole(CreateRoleRequestModel roleCreate);
        public RoleViewModel UpdateRole(UpdateRoleRequestModel roleUpdate);
        public bool DeleteRole(int idTmp);
        public List<RoleViewModel> GetAll();
        public RoleViewModel GetById(int idTmp);
    }

    public class RoleService : IRoleService
    {
        public RoleService()
        {
            
        }

        public RoleViewModel CreateRole(CreateRoleRequestModel roleCreate)
        {
            throw new NotImplementedException();
        }

        public RoleViewModel UpdateRole(UpdateRoleRequestModel roleUpdate)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRole(int idTmp)
        {
            throw new NotImplementedException();
        }

        public List<RoleViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public RoleViewModel GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

    }

}
