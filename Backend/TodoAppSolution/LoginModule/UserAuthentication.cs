using Microsoft.Data.SqlClient;
using System;

namespace LoginModule
{
    public class Authentication
    {
        public static bool AuthenticateUser(string connectionString, string username, string enteredPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string checkUsernameQuery = "SELECT COUNT(*) FROM Users WHERE Name = @Username";

                    using (SqlCommand checkUsernameCommand = new SqlCommand(checkUsernameQuery, connection))
                    {
                        checkUsernameCommand.Parameters.AddWithValue("@Username", username);

                        int userCount = (int)checkUsernameCommand.ExecuteScalar();

                        if (userCount == 0)
                        {
                            Console.WriteLine($"User with username '{username}' does not exist.");
                            return false;
                        }
                    }

                    string query = "SELECT Password FROM Users WHERE Name = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHashedPassword = reader["Password"].ToString();

                                string enteredHashedPassword = HashPassword.Hash(enteredPassword);

                                bool isValid = string.Equals(enteredHashedPassword, storedHashedPassword);

                                return isValid;
                            }
                            else
                            {
                                Console.WriteLine($"User with username {username} does not exist.");
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return false;
            }
        }
    }
}
