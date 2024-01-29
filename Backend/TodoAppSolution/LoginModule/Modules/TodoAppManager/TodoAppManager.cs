using LoginModule.Modules.TaskManager;
using LoginModule.Modules.TaskManagerModule;
using LoginModule.Modules.TaskTitle;
using LoginModule.Modules.TaskTitleManager;

public class TodoAppManager
{
    public static void Start(Guid userId)
    {
        Console.WriteLine($"\nWelcome  to our Todo Application, User ID: {userId}");

        while (true)
        {
            Console.WriteLine("\nTodo Application Menu:");

            Console.WriteLine("1. View Task Titles");
            Console.WriteLine("2. Insert Task Title");
            Console.WriteLine("3. View Tasks");
            Console.WriteLine("4. Insert Task");
            Console.WriteLine("5. Change my Status");
            Console.WriteLine("6. View only my Completed Tasks");
            Console.WriteLine("7. View particular Task");
            Console.WriteLine("8. Delete a particular Task");
            Console.WriteLine("9. Delete a particular Task Title");

            Console.WriteLine("10. Exit");

            Console.Write("Enter your choice (1-7): ");

            string? choice = Console.ReadLine();
            Console.WriteLine("\n");

            switch (choice)
            {
                case "1":
                    ViewTask.ViewTaskTitles();
                    break;
                case "2":
                    ViewTask.ViewTaskTitles();
                    AddTaskTitle.AddNewTaskTitle();
                    break;
                case "3":
                    ViewTaskbyUserId.ViewTasksWithDetails(userId);
                    break;
                case "4":
                    TaskInsertion.InsertTask(userId);
                    break;
                case "5":
                    Guid taskId = GetTaskIdFromUser();
                    ChangeStatus.MarkTaskAsCompleted(taskId);
                    break;
                case "6":
                    CheckStatus.GetCompletedTasks(userId);
                    break;
                case "7":
                    ViewTask.ViewTaskTitles();
                    Console.Write("Enter the task title: ");
                    int titleId = int.Parse(Console.ReadLine());
                    FilterTaskTitle.GetTasksByTitle(userId, titleId);
                    break;
                case "8":
                    ViewTaskbyUserId.ViewTasksWithDetails(userId);
                    Guid taskIdToDelete = GetTaskIdFromUser();
                    DeleteTask.DeleteTaskById(taskIdToDelete);
                    break;
                case "9":
                    ViewTask.ViewTaskTitles();
                    Console.Write("Enter the Task Title: ");
                    string titleNameToDelete = Console.ReadLine();
                    DeleteTaskTitle.DeleteTaskTitleByName(titleNameToDelete);
                    break;
                case "10":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    public static Guid GetTaskIdFromUser()
    {
        Console.Write("Enter the Task ID: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid taskId))
        {
            return taskId;
        }
        Console.WriteLine("Invalid Task ID");
        return GetTaskIdFromUser();
    }
}
