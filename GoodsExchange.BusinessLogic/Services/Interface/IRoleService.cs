namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IRoleService
    {
        Task<List<string>> GetRolesOfUser(Guid id);
        Task<Guid> GetRoleIdOfRoleName(string roleName);
        Task<bool> HasPermissionToReportAndRating(Guid from, Guid to);
    }
}
