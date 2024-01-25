using LoginModule.Modules.TaskTitle;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModule.Modules.TaskManager
{
    class TaskInsertion
    {
        public static void InsertTask(Guid userId)
        {

            Console.WriteLine("Select a Task Title:");

            List<TaskItem> availableTitles = ViewTask.ViewAllTaskTitles();
            int index = 1;

            foreach (var task in availableTitles)
            {
                Console.WriteLine($"{index}. {task.TitleName} (TitleId: {task.TitleId})");
                index++;
            }

            Guid titleId = GetTaskTitle.GetSelectedTitleId();

            Console.Write("Enter task description: ");
            string? description = Console.ReadLine();

            Console.WriteLine("Due Date Options:");
            Console.WriteLine("1. Today");
            Console.WriteLine("2. Tomorrow");
            Console.WriteLine("3. Custom Date");

            DateTime dueTime;

            switch (Console.ReadLine())
            {
                case "1":
                    dueTime = DateTime.Today;
                    break;
                case "2":
                    dueTime = DateTime.Today.AddDays(1);
                    break;
                case "3":
                    Console.Write("Enter custom due date (yyyy-MM-dd HH:mm): ");
                    if (DateTime.TryParse(Console.ReadLine(), out dueTime))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Setting due date to default (today).");
                        dueTime = DateTime.Today;
                        break;
                    }
                default:
                    Console.WriteLine("Invalid option. Setting due date to default (today).");
                    dueTime = DateTime.Today;
                    break;
            }

            string status = "Not Completed";

            InsertTaskToDatabase(userId,
                                 titleId,
                                 description,
                                 dueTime,
                                 status);
            Console.WriteLine("Task added successfully!");
        }

        public static void InsertTaskToDatabase(Guid userId, Guid titleId, string description, DateTime dueTime, string status)
        {
            try
            {
                ConnectionString connectionString = new ConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("InsertTask", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@TitleId", titleId);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@DueTime", dueTime);
                        command.Parameters.AddWithValue("@Status", status);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting task: {ex.Message}");
            }
        }
    }
}
