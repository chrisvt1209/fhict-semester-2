using Microsoft.Data.SqlClient;

namespace GameAttic.Infrastructure
{
    public abstract class BaseDb
    {
        protected string _connectionString;
        protected SqlConnection connection;

        protected BaseDb()
        {
            string? connectionString = Environment.GetEnvironmentVariable("GAMEATTIC_DB") ?? "Password=#$%GameAttic%$#;Persist Security Info=True;User ID=dbi515073_gameattic;Initial Catalog=dbi515073_gameattic;Data Source=mssqlstud.fhict.local;TrustServerCertificate=True";
            if (!string.IsNullOrEmpty(connectionString))
            {
                _connectionString = connectionString;
            }
            else
            {
                throw new InvalidOperationException("Database connection string is not set in the environment variable.");
            }
            connection = new SqlConnection(_connectionString);
        }
        protected void OpenConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(_connectionString);
            }

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
        protected void CloseConnection()
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        protected static void AddSqlParameter(SqlCommand command, string parameterName, object? value)
        {
            if (value != null && value.ToString() != "")
            {
                command.Parameters.Add(new SqlParameter(parameterName, value));
            }
        }
    }
}
