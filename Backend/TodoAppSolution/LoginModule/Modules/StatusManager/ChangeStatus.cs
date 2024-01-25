using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace LoginModule.Modules.TaskManagerModule
{
    class ChangeStatus
    {
        public static void MarkTaskAsCompleted(Guid taskId)
        {
            try
            {
                ConnectionString connectionString = new ConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ChangeTaskStatusToCompleted", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TaskId", taskId);

                        command.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Task status updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating task status: {ex.Message}");
            }
        }
    }
}
