using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly GoodsExchangeDbContext _context;
        public RoleService(GoodsExchangeDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> GetRoleIdOfRoleName(string roleName)
        {
            var role = await _context.Roles.Where(r => r.RoleName == roleName).FirstOrDefaultAsync();
            if (role == null)
            {
                throw new Exception("Role does not exist");
            }
            return role.RoleId;
        }
    }
}
