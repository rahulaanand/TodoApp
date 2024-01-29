using Microsoft.Data.SqlClient;

namespace LoginModule.Modules.TaskTitleManager
{
    class AddTaskTitle
    {
        public static Guid AddNewTaskTitle()
        {
            try
            {
                Console.Write("Enter the new Task Title: ");
                string newTitle = Console.ReadLine();

                ConnectionString connectionString = new ConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("InsertTaskTitle", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TitleName", newTitle);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Task Title added successfully!");                   
                        }
                        else
                        {
                            Console.WriteLine("Failed to add Task Title. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding new task title: {ex.Message}");
            }

            return Guid.Empty; 
        }
    }
}
