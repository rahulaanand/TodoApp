using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModule.Modules.TaskManagerModule
{
    class CheckStatus
    {
        public static void GetCompletedTasks(Guid userId)
        {
            try
            {
                ConnectionString connectionString = new ConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCompletedTasks", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Completed Tasks:");
                            while (reader.Read())
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving completed tasks: {ex.Message}");
            }
        }

    }
}
