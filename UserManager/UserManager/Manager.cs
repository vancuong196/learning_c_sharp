using System;
using System.Collections.Generic;


namespace UserManager
{

    class Manager
    {
        List<User> UserList;
        public Manager()
        {
            UserList = new List<User>();
        }
        public void AddUser()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Normal User");
            Console.WriteLine("2. Vip User");
            string intString = Console.ReadLine();
            byte option = 0;
            Byte.TryParse(intString, out option);
            switch (option)
            {
                case 1:
                    NormalUser user = new NormalUser();
                    user.input();
                    if (IsIDExist(user.ID))
                    {
                        Console.WriteLine("User Id is exist");
                        Console.WriteLine("Press any key to back to menu");
                        Console.ReadKey();
                        Console.Clear();
                        return;
                    }
                    this.UserList.Add(user);

                    break;
                case 2:
                    Vipuser vipuser = new Vipuser();
                    vipuser.input();
                    if (IsIDExist(vipuser.ID))
                    {
                        Console.WriteLine("User Id is exist");
                        Console.WriteLine("Press any key to back to menu");
                        Console.ReadKey();
                        Console.Clear();
                        return;
                    }
                    this.UserList.Add(vipuser);
                    break;

            }

            Console.WriteLine("Press any key to back to menu");
            Console.ReadKey();
            Console.Clear();

        }
        public void ShowUserList()
        {
            if (UserList.Count == 0)
            {
                Console.WriteLine("Empty list!");
            }
            else
            {
                foreach (User user in UserList)
                {
                    Console.WriteLine(user.GetInfo());
                }
            }

            Console.WriteLine("Press any key to back to menu");
            Console.ReadKey();
            Console.Clear();
        }
        public void RemoveUser()
        {
            Console.WriteLine("Enter an UserID to delete: ");
            string UserID = Console.ReadLine();
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].ID == UserID)
                {

                    Console.Write("Found and deleted 1: {0}", UserList[i].GetInfo());
                    UserList.RemoveAt(i);

                    break;
                }
            }
            Console.WriteLine("Press any key to back to menu");
            Console.ReadKey();
            Console.Clear();
        }
        public void FindUser()
        {
            Console.WriteLine("Enter a Name to find: ");
            string Username = Console.ReadLine();
            Console.WriteLine("Result: ");

            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].Name.Contains(Username) || UserList[i].Name == Username)
                {
                    Console.WriteLine("{0}", UserList[i].GetInfo());
                }
            }
            Console.WriteLine("Press any key to back to menu");
            Console.ReadKey();
            Console.Clear();
        }
        public bool IsIDExist(String id)
        {

            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].ID == id)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
