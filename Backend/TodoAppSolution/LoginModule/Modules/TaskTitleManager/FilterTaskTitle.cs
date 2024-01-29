using System;
using System.Data;
using LoginModule.Modules.TaskTitleManager;
using Microsoft.Data.SqlClient;

namespace LoginModule.Modules.TaskTitle
{
    class FilterTaskTitle
    {
        public static void GetTasksByTitle(Guid userId, int titleIndex)
        {
            try
            {
                List<TaskItem> availableTitles = ViewTask.ViewAllTaskTitles();

                if (titleIndex >= 1 && titleIndex <= availableTitles.Count)
                {
                    Guid titleId = availableTitles[titleIndex - 1].TitleId;

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
                                Console.WriteLine($"Tasks with Title '{availableTitles[titleIndex - 1].TitleName}':");
                                while (reader.Read())
                                {
                                    if (reader.IsDBNull(0) == false && reader.IsDBNull(5) == false)
                                    {
                                        Guid taskId = reader.GetGuid(0);
                                        string taskTitle = reader.GetString(5);

                                        if (taskTitle != null && taskTitle.Equals(availableTitles[titleIndex - 1].TitleName, StringComparison.OrdinalIgnoreCase))
                                        {
                                            Console.WriteLine("Task Details:");
                                            Console.WriteLine($"  Task ID: {taskId}");
                                            Console.WriteLine($"  Title: {taskTitle}");
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
                }
                else
                {
                    Console.WriteLine("Invalid Task Title Index.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tasks by title: {ex.Message}");
            }
        }
    }
}
