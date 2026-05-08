using AccessManagementWebApp.Models;
using Oracle.ManagedDataAccess.Client;

namespace AccessManagementWebApp.Repositories
{
    /// <summary>
    /// Repository for managing user access operations using Oracle database.
    /// </summary>
    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly OracleSettings _settings;
        private readonly Dictionary<string, string> _functionTableMappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccessRepository"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public UserAccessRepository(IConfiguration configuration)
        {
            _settings = configuration.GetSection("Oracle").Get<OracleSettings>()
                        ?? throw new InvalidOperationException("Oracle configuration section is missing.");

            _functionTableMappings = configuration.GetSection("FunctionTableMappings").Get<Dictionary<string, string>>()
                                     ?? throw new InvalidOperationException("Function table mappings configuration section is missing.");
        }

        /// <summary>
        /// Creates a new Oracle database connection.
        /// </summary>
        /// <returns>An Oracle connection instance.</returns>
        private OracleConnection CreateConnection()
        {
            return new OracleConnection(_settings.GetConnectionString());
        }

        /// <summary>
        /// Gets the table name for the specified function.
        /// </summary>
        /// <param name="functionName">The function name.</param>
        /// <returns>The corresponding table name.</returns>
        /// <exception cref="ArgumentNullException">Thrown if functionName is null.</exception>
        /// <exception cref="ArgumentException">Thrown if functionName is not supported.</exception>
        private string GetUserAccessTableName(string functionName)
        {
            if (functionName is null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }

            var key = functionName.Trim();
            if (_functionTableMappings.TryGetValue(key, out var tableName))
            {
                return tableName;
            }

            var comparer = StringComparer.OrdinalIgnoreCase;
            var match = _functionTableMappings.Keys.FirstOrDefault(k => comparer.Equals(k, key));
            if (match != null && _functionTableMappings.TryGetValue(match, out tableName))
            {
                return tableName;
            }

            throw new ArgumentException($"Unsupported function name: {functionName}", nameof(functionName));
        }

        /// <summary>
        /// Checks if a user has access to a specific function.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <param name="functionName">The function name to check access for.</param>
        /// <returns>The access level if access exists, otherwise null.</returns>
        public async Task<string?> CheckUserAccessAsync(string username, string functionName)
        {
            var tableName = GetUserAccessTableName(functionName);
            var sql = $@"SELECT access_level FROM {tableName}
                                 WHERE username = :username
                                   AND application_id = :functionName";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("functionName", functionName));

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.IsDBNull(0) ? null : reader.GetString(0);
            }

            return null;
        }

        /// <summary>
        /// Creates access for a user to a specific function.
        /// </summary>
        /// <param name="username">The username to grant access to.</param>
        /// <param name="functionName">The function name to grant access for.</param>
        /// <returns>True if access was created successfully, otherwise false.</returns>
        public async Task<bool> CreateAccessAsync(string username, string functionName)
        {
            var tableName = GetUserAccessTableName(functionName);
            var sql = $@"INSERT INTO {tableName} (username, application_id, access_level)
                                 VALUES (:username, :functionName, :accessLevel)";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("functionName", functionName));
            command.Parameters.Add(new OracleParameter("accessLevel", "User"));

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        /// <summary>
        /// Revokes access for a user from a specific function.
        /// </summary>
        /// <param name="username">The username to revoke access from.</param>
        /// <param name="functionName">The function name to revoke access for.</param>
        /// <returns>True if access was revoked successfully, otherwise false.</returns>
        public async Task<bool> RevokeAccessAsync(string username, string functionName)
        {
            var tableName = GetUserAccessTableName(functionName);
            var sql = $@"DELETE FROM {tableName}
                                 WHERE username = :username
                                   AND application_id = :functionName";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("functionName", functionName));

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        /// <summary>
        /// Updates the permission level for a user's access to a specific function.
        /// </summary>
        /// <param name="username">The username to update access for.</param>
        /// <param name="functionName">The function name to update access for.</param>
        /// <param name="newPermission">The new permission level.</param>
        /// <returns>True if access was updated successfully, otherwise false.</returns>
        public async Task<bool> UpdateAccessAsync(string username, string functionName, string newPermission)
        {
            var tableName = GetUserAccessTableName(functionName);
            var sql = $@"UPDATE {tableName}
                                 SET access_level = :newPermission
                                 WHERE username = :username
                                   AND application_id = :functionName";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("newPermission", newPermission));
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("functionName", functionName));

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        /// <summary>
        /// Reactivates access for a user to a specific function.
        /// </summary>
        /// <param name="username">The username to reactivate access for.</param>
        /// <param name="functionName">The function name to reactivate access for.</param>
        /// <returns>True if access was reactivated successfully, otherwise false.</returns>
        public async Task<bool> ReactivateAccessAsync(string username, string functionName)
        {
            var tableName = GetUserAccessTableName(functionName);
            var sql = $@"INSERT INTO {tableName} (username, application_id, access_level)
                                 VALUES (:username, :functionName, :accessLevel)";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("functionName", functionName));
            command.Parameters.Add(new OracleParameter("accessLevel", "User"));

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }
}
