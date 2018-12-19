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
        public void Input()
        {
            Console.WriteLine("Please input ID:");

            string inputID = Console.ReadLine();

            Console.WriteLine("Please input name:");

            string inputName = Console.ReadLine();
         
            Console.WriteLine("Please input age:");

            string ageAsString = Console.ReadLine();

            int inputAge = 0;

            Int32.TryParse(ageAsString, out inputAge);

            this.Name = inputName;

            this.Age = inputAge;

            this.ID = inputID;

        }

    }
}
