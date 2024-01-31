using LoginModule.Modules.TaskTitleManager;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LoginModule.Modules.TaskManager
{
    class TaskInsertion
    {
        public static void InsertTask(Guid userId)
        {
            try
            {
                Console.WriteLine("Do you want to add a new Task Title? (yes/no): ");
                string addNewTitleChoice = Console.ReadLine().ToLower();

                Guid titleId;

                if (addNewTitleChoice == "yes")
                {
                    titleId = AddTaskTitle.AddNewTaskTitle();
                    List<TaskItem> availableTitles = ViewTask.ViewAllTaskTitles();
                    int index = 1;

                    foreach (var task in availableTitles)
                    {
                        Console.WriteLine($"{index}. {task.TitleName} (TitleId: {task.TitleId})");
                        index++;
                    }

                    Console.Write("Enter your choice: ");
                    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= index)
                    {
                        titleId = availableTitles[choice - 1].TitleId;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                        titleId = Guid.Empty;
                    }
                }

                else if (addNewTitleChoice == "no")
                {
                    Console.WriteLine("Select a Task Title:");

                    List<TaskItem> availableTitles = ViewTask.ViewAllTaskTitles();
                    int index = 1;

                    foreach (var task in availableTitles)
                    {
                        Console.WriteLine($"{index}. {task.TitleName} (TitleId: {task.TitleId})");
                        index++;
                    }

                    Console.Write("Enter your choice: ");
                    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= index)
                    {
                        titleId = availableTitles[choice - 1].TitleId;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                        titleId = Guid.Empty;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                    titleId = Guid.Empty;
                }

                Console.Write("Enter task description: ");
                string? description = Console.ReadLine();

                while (string.IsNullOrWhiteSpace(description))
                {
                    Console.WriteLine("Description cannot be empty. Please enter a valid description.");
                    description = Console.ReadLine();
                }

                Console.WriteLine("Due Date Options:");
                Console.WriteLine("1. Today");
                Console.WriteLine("2. Tomorrow");
                Console.WriteLine("3. Custom Date");

                DateTime dueTime;

                string dueDateOption = Console.ReadLine();

                switch (dueDateOption)
                {
                    case "1":
                        dueTime = DateTime.Today;
                        break;
                    case "2":
                        dueTime = DateTime.Today.AddDays(1);
                        break;
                    case "3":
                        Console.Write("Enter custom due date (yyyy-MM-dd HH:mm): ");
                        while (!DateTime.TryParse(Console.ReadLine(), out dueTime) || dueTime < DateTime.Now)
                        {
                            Console.WriteLine("Invalid date");
                            Console.Write("Enter custom due date (yyyy-MM-dd HH:mm): ");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option. Setting due date to default");
                        dueTime = DateTime.Today;
                        break;
                }

                string status = "Not Completed";

                InsertTaskToDatabase(userId, titleId, description, dueTime, status);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting task: {ex.Message}");
            }
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
                        Console.WriteLine("Task added successfully");
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


