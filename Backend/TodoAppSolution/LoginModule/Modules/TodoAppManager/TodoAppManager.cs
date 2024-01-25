using LoginModule.Modules.TaskManager;
using LoginModule.Modules.TaskManagerModule;
using LoginModule.Modules.TaskTitle;

public class TodoAppManager
{
    public static void Start(Guid userId)
    {
        Console.WriteLine($"Welcome to our Todo Application, User ID: {userId}");

        while (true)
        {
            Console.WriteLine("Todo Application Menu:");
            Console.WriteLine("1. View Task Titles");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Insert Task");
            Console.WriteLine("4. Change my Status");
            Console.WriteLine("5. View only my Completed Tasks");
            Console.WriteLine("6. View particular Task");
            Console.WriteLine("7. Exit");


            Console.Write("Enter your choice (1-5): ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewTask.ViewTaskTitles();
                    break;
                case "2":
                    ViewTaskbyUserId.ViewTasksWithDetails(userId);
                    break;
                case "3":
                    TaskInsertion.InsertTask(userId);
                    break;
                case "4":
                    Guid taskId = GetTaskIdFromUser();
                    ChangeStatus.MarkTaskAsCompleted(taskId);
                    break;
                case "5":
                    CheckStatus.GetCompletedTasks(userId);
                    break;
                case "6":
                    ViewTask.ViewTaskTitles();
                    Console.Write("Enter the task title: ");
                    string titleName = Console.ReadLine();
                    FilterTaskTitle.GetTasksByTitle(userId, titleName);
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    private static Guid GetTaskIdFromUser()
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
