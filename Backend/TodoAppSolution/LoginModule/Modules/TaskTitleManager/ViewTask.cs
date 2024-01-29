using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

namespace LoginModule.Modules.TaskTitleManager
{
    class ViewTask
    {
        public static void ViewTaskTitles()
        {
            List<TaskItem> availableTitles = ViewAllTaskTitles();

            Console.WriteLine("Available Task Titles:");

            int index = 1;

            foreach (var task in availableTitles)
            {
                Console.WriteLine($"{index}. {task.TitleName}");
                index++;
            }
        }

        public static List<TaskItem> ViewAllTaskTitles()
        {
            List<TaskItem> availableTitles = new List<TaskItem>();

            try
            {
                ConnectionString connectionString = new ConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ViewAllTaskTitles", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TaskItem task = new TaskItem
                                {
                                    TitleId = reader.GetGuid(0),
                                    TitleName = reader.GetString(1)
                                };

                                availableTitles.Add(task);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving task titles: {ex.Message}");
            }

            return availableTitles;
        }
    }
}
