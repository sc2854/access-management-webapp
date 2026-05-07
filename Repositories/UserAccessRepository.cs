using AccessManagementWebApp.Models;
using Oracle.ManagedDataAccess.Client;

namespace AccessManagementWebApp.Repositories
{
    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly OracleSettings _settings;

        public UserAccessRepository(IConfiguration configuration)
        {
            _settings = configuration.GetSection("Oracle").Get<OracleSettings>()
                        ?? throw new InvalidOperationException("Oracle configuration section is missing.");
        }

        private OracleConnection CreateConnection()
        {
            return new OracleConnection(_settings.GetConnectionString());
        }

        public async Task<string?> CheckUserAccessAsync(string username, int applicationId)
        {
            const string sql = @"SELECT access_level FROM user_access
                                 WHERE username = :username
                                   AND application_id = :applicationId";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("applicationId", applicationId));

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.IsDBNull(0) ? null : reader.GetString(0);
            }

            return null;
        }

        public async Task<bool> CreateAccessAsync(string username, int applicationId)
        {
            const string sql = @"INSERT INTO user_access (username, application_id, access_level)
                                 VALUES (:username, :applicationId, :accessLevel)";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("applicationId", applicationId));
            command.Parameters.Add(new OracleParameter("accessLevel", "User"));

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> RevokeAccessAsync(string username, int applicationId)
        {
            const string sql = @"DELETE FROM user_access
                                 WHERE username = :username
                                   AND application_id = :applicationId";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("applicationId", applicationId));

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateAccessAsync(string username, int applicationId, string newPermission)
        {
            const string sql = @"UPDATE user_access
                                 SET access_level = :newPermission
                                 WHERE username = :username
                                   AND application_id = :applicationId";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("newPermission", newPermission));
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("applicationId", applicationId));

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> ReactivateAccessAsync(string username, int applicationId)
        {
            const string sql = @"INSERT INTO user_access (username, application_id, access_level)
                                 VALUES (:username, :applicationId, :accessLevel)";

            await using var connection = CreateConnection();
            await connection.OpenAsync();
            await using var command = new OracleCommand(sql, connection);
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("applicationId", applicationId));
            command.Parameters.Add(new OracleParameter("accessLevel", "User"));

            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }
}
