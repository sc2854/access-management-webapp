namespace AccessManagementWebApp.Repositories
{
    /// <summary>
    /// Interface for user access repository operations.
    /// </summary>
    public interface IUserAccessRepository
    {
        /// <summary>
        /// Checks if a user has access to a specific function.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <param name="functionName">The function name to check access for.</param>
        /// <returns>The access level if access exists, otherwise null.</returns>
        Task<string?> CheckUserAccessAsync(string username, string functionName);

        /// <summary>
        /// Creates access for a user to a specific function.
        /// </summary>
        /// <param name="username">The username to grant access to.</param>
        /// <param name="functionName">The function name to grant access for.</param>
        /// <returns>True if access was created successfully, otherwise false.</returns>
        Task<bool> CreateAccessAsync(string username, string functionName);

        /// <summary>
        /// Revokes access for a user from a specific function.
        /// </summary>
        /// <param name="username">The username to revoke access from.</param>
        /// <param name="functionName">The function name to revoke access for.</param>
        /// <returns>True if access was revoked successfully, otherwise false.</returns>
        Task<bool> RevokeAccessAsync(string username, string functionName);

        /// <summary>
        /// Updates the permission level for a user's access to a specific function.
        /// </summary>
        /// <param name="username">The username to update access for.</param>
        /// <param name="functionName">The function name to update access for.</param>
        /// <param name="newPermission">The new permission level.</param>
        /// <returns>True if access was updated successfully, otherwise false.</returns>
        Task<bool> UpdateAccessAsync(string username, string functionName, string newPermission);

        /// <summary>
        /// Reactivates access for a user to a specific function.
        /// </summary>
        /// <param name="username">The username to reactivate access for.</param>
        /// <param name="functionName">The function name to reactivate access for.</param>
        /// <returns>True if access was reactivated successfully, otherwise false.</returns>
        Task<bool> ReactivateAccessAsync(string username, string functionName);
    }
}
