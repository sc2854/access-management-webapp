namespace AccessManagementWebApp.Repositories
{
    public interface IUserAccessRepository
    {
        Task<string?> CheckUserAccessAsync(string username, int applicationId);
        Task<bool> CreateAccessAsync(string username, int applicationId);
        Task<bool> RevokeAccessAsync(string username, int applicationId);
        Task<bool> UpdateAccessAsync(string username, int applicationId, string newPermission);
        Task<bool> ReactivateAccessAsync(string username, int applicationId);
    }
}
