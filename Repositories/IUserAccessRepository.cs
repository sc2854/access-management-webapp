namespace AccessManagementWebApp.Repositories
{
    public interface IUserAccessRepository
    {
        Task<string?> CheckUserAccessAsync(string username, string functionName);
        Task<bool> CreateAccessAsync(string username, string functionName);
        Task<bool> RevokeAccessAsync(string username, string functionName);
        Task<bool> UpdateAccessAsync(string username, string functionName, string newPermission);
        Task<bool> ReactivateAccessAsync(string username, string functionName);
    }
}
