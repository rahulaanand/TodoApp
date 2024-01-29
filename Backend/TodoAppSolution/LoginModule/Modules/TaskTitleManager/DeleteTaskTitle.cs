using LoginModule.Modules.TaskManager;
using Microsoft.Data.SqlClient;
using System;

namespace LoginModule.Modules.TaskTitleManager
{
    internal class DeleteTaskTitle
    {
        public static void DeleteTaskTitleByName(string titleNameToDelete)
        {
            try
            {
                ConnectionString connectionString = new ConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("DeleteTaskTitleName", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TitleName", titleNameToDelete);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Task Title deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to delete Task Title. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting task title: {ex.Message}");
            }
        }
    }
}
