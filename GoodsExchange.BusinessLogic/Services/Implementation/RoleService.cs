using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Category;
using GoodsExchange.BusinessLogic.ViewModels.Role;
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
                throw new NotFoundException("Role does not exist");
            }
            return role.RoleId;
        }
        public async Task<EntityResponse<List<RoleViewModel>>> GetAllRole()
        {
            var Role = await _context.Roles.ToListAsync();
            var result =  Role.Select(r => new RoleViewModel
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,

            }).ToList();
            return new ApiSuccessResult<List<RoleViewModel>>(result);
        }
    }
}
