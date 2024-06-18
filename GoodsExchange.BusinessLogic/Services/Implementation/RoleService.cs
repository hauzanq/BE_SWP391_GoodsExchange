using GoodsExchange.BusinessLogic.Constants;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IServiceWrapper _serviceWrapper;
        public RoleService(GoodsExchangeDbContext context, IServiceWrapper serviceWrapper)
        {
            _context = context;
            _serviceWrapper = serviceWrapper;
        }
        public async Task<bool> HasPermissionToReportAndRating(Guid from, Guid to)
        {
            var receiver = await _serviceWrapper.UserServices.GetUserAsync(to);
            if (receiver.UserId == from)
            {
                return false;
            }
            if (receiver.UserRoles.Any(ur => ur.Role.RoleName.Equals(SystemConstant.Roles.Administrator)
                || ur.Role.RoleName.Equals(SystemConstant.Roles.Moderator)))
            {
                return false;
            }
            return true;
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

        public async Task<List<string>> GetRolesOfUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new List<string>();
            }

            var roles = await _context.UserRoles.Where(ur => ur.User == user).Select(ur => ur.Role).ToListAsync();

            var result = roles.Select(r => r.RoleName).ToList();
            return result;
        }
    }
}
