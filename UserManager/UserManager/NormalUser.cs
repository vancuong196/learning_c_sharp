using System;

namespace UserManager
{
    class NormalUser: User,IInputtable
    {
        public override string GetInfo()
        {
            string info = "ID: " + this.ID + "| name: " + this.Name + " | " + " age: " + this.Age + " | type = normal";
            return info;
        }
        public void input()
        {
            Console.WriteLine("Please input ID:");

            string id = Console.ReadLine();

            Console.WriteLine("Please input name:");

            string name = Console.ReadLine();
         
            Console.WriteLine("Please input age:");

            string ageAsString = Console.ReadLine();

            int age = 0;

            Int32.TryParse(ageAsString, out age);

            this.Name = name;

            this.Age = age;

            this.ID = id;

        }

    }
}
