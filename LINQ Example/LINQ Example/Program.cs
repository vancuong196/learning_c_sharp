using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Re-generate student database.");
                Console.WriteLine("2. Show all student.");
                Console.WriteLine("3. Search students.");
                Console.WriteLine("4. Show class list.");
                Console.WriteLine("5. Show The best students.");
                Console.WriteLine("6. Analyze.");
                Console.WriteLine("7. Exit.");
                int choice = 0;
                Int32.TryParse(Console.ReadLine(), out choice);
                if (choice == 1)
                {
                    Console.Clear();
                    Console.WriteLine("How many student do you need?");
                    int numberOfStudent = 1;
                    Int32.TryParse(Console.ReadLine(), out numberOfStudent);
                    manager = new Manager(numberOfStudent);
                }
                else if (choice == 2)
                {
                    manager.ShowAll();
                }
                else if (choice == 3)
                {
                    manager.Search();

                }
                else if (choice == 4)
                {
                    manager.ShowClass();
                }
                else if (choice == 5)
                {
                    manager.ShowTopStudents();
                }
                else if (choice == 6)
                {
                    manager.Analyze();
                }
                else if (choice == 7)
                {
                    Environment.Exit(0);
                }
            }

        }
    }
}
