using Oracle.ManagedDataAccess.Client;

namespace AccessManagementWebApp.Models
{
    /// <summary>
    /// Settings for Oracle database connection.
    /// </summary>
    public class OracleSettings
    {
        /// <summary>
        /// Gets or sets the Oracle user ID.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Oracle password.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Oracle data source (connection string).
        /// </summary>
        public string DataSource { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to persist security info in the connection string.
        /// </summary>
        public bool PersistSecurityInfo { get; set; }

        /// <summary>
        /// Builds and returns the Oracle connection string.
        /// </summary>
        /// <returns>The formatted connection string.</returns>
        public string GetConnectionString()
        {
            var builder = new OracleConnectionStringBuilder
            {
                UserID = UserId,
                Password = Password,
                DataSource = DataSource,
                PersistSecurityInfo = PersistSecurityInfo
            };

            return builder.ToString();
        }
    }
}
