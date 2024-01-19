using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace LoginModule
{
    public class LoginDetails
    {
        public static void login()
        {
            Console.WriteLine("Welcome to our Todo Application");

            ConnectionString connectionString = new ConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT TitleName FROM TaskTitle", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> titles = new List<string>();

                        while (reader.Read())
                        {
                            string title = reader.GetString(0);
                            titles.Add(title);
                        }

                        Console.WriteLine("Todo Titles:");

                        foreach (var title in titles)
                        {
                            Console.WriteLine(title);
                        }
                    }
                }
            }
        }
    }
}
