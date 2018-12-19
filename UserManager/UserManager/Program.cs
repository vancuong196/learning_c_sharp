using System;

namespace UserManager
{
    class Program
    {
     
        static void Main(string[] args)
        {
            var manager = new Manager();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Show User List");
                Console.WriteLine("3. Remove User ");
                Console.WriteLine("4. Find a user");
                Console.WriteLine("5. Exit");
                String optionAsString = Console.ReadLine();
                byte option = 0;
                Byte.TryParse(optionAsString, out option);
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        manager.AddUser();
                        break;
                    case 2:
                        Console.Clear();
                        manager.ShowUserList();
                        break;
                    case 3:
                        Console.Clear();
                        manager.RemoveUser();
                        break;
                    case 4:
                        Console.Clear();
                        manager.FindUser();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Unknown option");
                        Console.ReadKey();
                        break;


                }


            }
        }
    }
}
