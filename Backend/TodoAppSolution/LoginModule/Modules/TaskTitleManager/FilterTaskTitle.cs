using System.Data;
using Microsoft.Data.SqlClient;

namespace LoginModule.Modules.TaskTitle
{
    class FilterTaskTitle
    {
        public static void GetTasksByTitle(Guid userId, string titleName)
        {
            try
            {
                ConnectionString connectionString = new ConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ViewTasksWithDetailsByUserId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine($"Tasks with Title '{titleName}':");
                            while (reader.Read())
                            {
                                if (reader.GetString(5) == titleName)
                                {
                                    Console.WriteLine("Task Details:");
                                    Console.WriteLine($"  Task ID: {reader.GetGuid(0)}");
                                    Console.WriteLine($"  Title: {reader.GetString(5)}");
                                    Console.WriteLine($"  Description: {reader.GetString(1)}");
                                    Console.WriteLine($"  Created At: {reader.GetDateTime(2)}");
                                    Console.WriteLine($"  Due Time: {reader.GetDateTime(3)}");
                                    Console.WriteLine($"  Status: {reader.GetString(4)}");
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tasks by title: {ex.Message}");
            }
        }
    }
}
