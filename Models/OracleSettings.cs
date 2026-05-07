namespace AccessManagementWebApp.Models
{
    public class OracleSettings
    {
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DataSource { get; set; } = string.Empty;

        public string GetConnectionString()
        {
            return $"User Id={UserId};Password={Password};Data Source={DataSource}";
        }
    }
}
