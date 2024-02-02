using Microsoft.Data.SqlClient;
using System.Data;

namespace LoginModule.Modules.TaskManager
{
    class ViewTaskbyUserId
    {
        public static void ViewTasksWithDetails(Guid userId)
        {
            List<TaskItem> tasks = GetTasksWithDetails(userId);

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var task in tasks)
            {
                Console.WriteLine("Task Details:");
                Console.WriteLine($"  Task ID: {task.TitleId}");
                Console.WriteLine($"  Title: {task.TitleName}");
                Console.WriteLine($"  Description: {task.Description}");
                Console.WriteLine($"  Created At: {task.CreatedAt}");
                Console.WriteLine($"  Due Time: {task.DueTime}");
                Console.WriteLine($"  Status: {task.Status}");
                Console.WriteLine();
            }
        }

        public static List<TaskItem> GetTasksWithDetails(Guid userId)
        {
            List<TaskItem> tasks = new List<TaskItem>();

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
                            while (reader.Read())
                            {
                                TaskItem task = new TaskItem
                                {
                                    TitleId = reader.GetGuid(0),
                                    Description = reader.GetString(1),
                                    CreatedAt = reader.GetDateTime(2),
                                    DueTime = reader.GetDateTime(3),
                                    Status = reader.GetString(4),
                                    TitleName = reader.GetString(5),
                                };

                                tasks.Add(task);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tasks with details: {ex.Message}");
            }
            return tasks;
        }
    }
}
