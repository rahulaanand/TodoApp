using Microsoft.Data.SqlClient;

namespace LoginModule
{
    public class RegisterUser
    {
        public static void RegisterUsers(string connectionString, string username, string password, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertUser", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", username);
                    command.Parameters.AddWithValue("@Password", HashPassword.Hash(password));
                    command.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }
    }
}
