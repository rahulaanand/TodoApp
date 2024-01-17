using Microsoft.Data.SqlClient;

namespace LoginModule
{
    public class Authentication
    {
        public static bool IsUserRegistered(string connectionString, string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Name = @Username AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int userCount = (int)command.ExecuteScalar();

                        return (userCount > 0);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }


            }

        }
    }
}
