using LoginModule.Modules.TaskTitleManager;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModule.Modules.TaskTitle
{
    internal class GetTaskTitle
    {
        public static string GetTaskTitleById(Guid titleId)
        {
            try
            {
                ConnectionString connectionString = new ConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ViewTaskTitleById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TitleId", titleId);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving task title by Id: {ex.Message}");
            }

            return "Title Not Found";
        }

        public static Guid GetSelectedTitleId()
        {
            Console.Write("Enter the Task Title Index from the above list: ");
            if (int.TryParse(Console.ReadLine(), out int titleIndex))
            {
                List<TaskItem> availableTitles = ViewTask.ViewAllTaskTitles();

                if (titleIndex >= 1 && titleIndex <= availableTitles.Count)
                {
                    return availableTitles[titleIndex - 1].TitleId;
                }
            }

            Console.WriteLine("Invalid Task Title Index. Please try again.");
            return GetSelectedTitleId();
        }

    }
}
