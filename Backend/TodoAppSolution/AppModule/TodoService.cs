//// TodoService.cs in AppModule

//using System;
//using LoginModule;
//using Microsoft.Data.SqlClient;

//namespace AppModule
//{
//    public class TodoService
//    {
//        public static void DisplayTodoList()
//        {
//            Console.WriteLine("Welcome to the App Module!");

//            ConnectionString connectionString = new ConnectionString();

//            using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
//            {
//                connection.Open();

//                using (SqlCommand command = new SqlCommand("SELECT TitleName FROM TaskTitle", connection))
//                {
//                    using (SqlDataReader reader = command.ExecuteReader())
//                    {
//                        Console.WriteLine("Todo Titles:");

//                        while (reader.Read())
//                        {
//                            string title = reader.GetString(0);
//                            Console.WriteLine(title);
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
