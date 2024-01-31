using Microsoft.Data.SqlClient;

namespace LoginModule.Modules.TaskManager
{
    public class DeleteTask
    {
        public static void DeleteTaskById(Guid taskId)
        {
            try
            {
                ConnectionString connectionString = new ConnectionString();


                using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("DeleteTask", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TaskId", taskId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Task deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Task not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting task: {ex.Message}");
            }
        }
    }
}
