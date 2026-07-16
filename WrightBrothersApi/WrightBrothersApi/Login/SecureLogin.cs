using System.Data.SqlClient;

namespace WrightBrothersApi.Login;

/// <summary>
/// Provides login validation against a SQL Server Users table.
/// </summary>
public class SecureLogin
{
    /// <summary>
    /// Connection string used to open SQL Server connections.
    /// </summary>
    private readonly string _connectionString;

    /// <summary>
    /// Initializes the service with a database connection string.
    /// </summary>
    /// <param name="connectionString">SQL Server connection string.</param>
    public SecureLogin(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Checks whether a user exists with the provided credentials.
    /// </summary>
    /// <param name="username">Username to validate.</param>
    /// <param name="password">Password to validate.</param>
    /// <returns><c>true</c> when a matching user is found; otherwise <c>false</c>.</returns>
    /// <remarks>
    /// This method currently uses dynamic SQL and is vulnerable to SQL injection.
    /// Parameterized queries should be used for production code.
    /// </remarks>
    public bool AuthenticateUser(string username, string password)
    {
        string query = "SELECT * FROM Users WHERE Username = '" + username + "' AND Password = '" + password + "'";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }

                reader.Close();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}

