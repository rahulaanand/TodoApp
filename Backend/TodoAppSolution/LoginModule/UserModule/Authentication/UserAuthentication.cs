using LoginModule.LoginModule;
using Microsoft.Data.SqlClient;

public class Authentication
{
    public static Guid AuthenticateUser(string connectionString, string username, string enteredPassword)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string checkUsernameQuery = "SELECT Id, Password FROM Users WHERE Name = @Username";

                using (SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, connection))
                {
                    checkUsernameCommand.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = checkUsernameCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Guid userId = reader.GetGuid(0);
                            string storedHashedPassword = reader["Password"].ToString();

                            string enteredHashedPassword = HashPassword.Hash(enteredPassword);

                            if (string.Equals(enteredHashedPassword, storedHashedPassword))
                            {
                                return userId;
                            }
                            else
                            {
                                Console.WriteLine("Invalid password.");
                                return Guid.Empty;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"User with username '{username}' does not exist.");
                            return Guid.Empty;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during login: {ex.Message}");
            return Guid.Empty;
        }
    }
}
