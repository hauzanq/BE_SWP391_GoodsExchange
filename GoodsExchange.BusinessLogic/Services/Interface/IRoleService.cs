namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IRoleService
    {
        Task<Guid> GetRoleIdOfRoleName(string roleName);
    }
}
