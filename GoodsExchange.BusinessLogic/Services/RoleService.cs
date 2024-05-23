using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Role;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services
{
    public interface IRoleService
    {
        Task<ApiResult<RoleViewModel>> CreateRole(CreateRoleRequestModel request);
        Task<ApiResult<RoleViewModel>> UpdateRole(UpdateRoleRequestModel request);
        Task<ApiResult<bool>> DeleteRole(Guid id);
        Task<ApiResult<List<RoleViewModel>>> GetAll();
        Task<ApiResult<RoleViewModel>> GetById(Guid id);
        Task<ApiResult<List<RoleViewModel>>> GetUserRole(Guid id);
    }

    public class RoleService : IRoleService
    {
        private readonly GoodsExchangeDbContext _context;

        public RoleService(GoodsExchangeDbContext context)
        {
            _context = context;
        }

        public Task<ApiResult<RoleViewModel>> CreateRole(CreateRoleRequestModel request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> DeleteRole(Guid idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<RoleViewModel>> GetById(Guid idTmp)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetUserRole(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ApiErrorResult<List<RoleViewModel>>("User does not exists");
            }

            var roles = await _context.UserRoles.Where(ur =>ur.User == user).Select(ur => ur.Role).ToListAsync();

            var result = roles.Select(r => new RoleViewModel()
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
            }).ToList();

            return new ApiSuccessResult<List<RoleViewModel>>(result);
        }

        public Task<ApiResult<RoleViewModel>> UpdateRole(UpdateRoleRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
