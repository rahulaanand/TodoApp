using System;

namespace LoginModule
{
    class Program
    {
        public static void Main()
        {
            ConnectionString connectionString = new ConnectionString();

            bool continueLoop = true;

            while (continueLoop)
            {
                Console.WriteLine("Are you already a member? (yes/no)");
                string response = Console.ReadLine().ToLower();

                if (response == "yes")
                {
                    Console.WriteLine("Enter your username:");
                    string username = Console.ReadLine();

                    Console.WriteLine("Enter your password:");
                    string password = Console.ReadLine();

                    if (Authentication.AuthenticateUser(connectionString.connectionString, username, password))
                    {
                        LoginDetails.login();
                    }
                    else
                    {
                        Console.WriteLine("User not found. Please register to log in.");
                    }
                }
                else if (response == "no")
                {
                    Console.WriteLine("You're not a member. Please register.");

                    Console.WriteLine("Enter your username:");
                    string username = Console.ReadLine();

                    Console.WriteLine("Enter your password:");
                    string password = Console.ReadLine();

                    Console.WriteLine("Enter your email:");
                    string email = Console.ReadLine();

                    RegisterUser.RegisterUsers(connectionString.connectionString, username, password, email);

                    Console.WriteLine("Registration successful. You can now log in.");
                }
                else
                {
                    Console.WriteLine("Invalid response. Please enter 'yes' or 'no'.");
                }
                Console.WriteLine("Do you want to continue? (yes/no)");
                string continueResponse = Console.ReadLine().ToLower();
                continueLoop = (continueResponse == "yes");
            }
        }
    }
}
