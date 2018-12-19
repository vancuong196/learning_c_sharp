using System;
using System.Collections.Generic;


namespace UserManager
{

    class Manager
    {
        private List<User> _userList;
        public Manager()
        {
            _userList = new List<User>();
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
                    var user = new NormalUser();
                    user.Input();
                    if (IsIDExist(user.ID))
                    {
                        Console.WriteLine("User Id is exist");
                        Console.WriteLine("Press any key to back to menu");
                        Console.ReadKey();
                        Console.Clear();
                        return;
                    }
                    this._userList.Add(user);

                    break;
                case 2:
                    var vipuser = new Vipuser();
                    vipuser.Input();
                    if (IsIDExist(vipuser.ID))
                    {
                        Console.WriteLine("User Id is exist");
                        Console.WriteLine("Press any key to back to menu");
                        Console.ReadKey();
                        Console.Clear();
                        return;
                    }
                    this._userList.Add(vipuser);
                    break;

            }

            Console.WriteLine("Press any key to back to menu");
            Console.ReadKey();
            Console.Clear();

        }
        public void ShowUserList()
        {
            if (_userList.Count == 0)
            {
                Console.WriteLine("Empty list!");
            }
            else
            {
                foreach (User user in _userList)
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
            for (int i = 0; i < _userList.Count; i++)
            {
                if (_userList[i].ID == UserID)
                {

                    Console.WriteLine("Found and deleted 1: {0}", _userList[i].GetInfo());
                    _userList.RemoveAt(i);

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

            for (int i = 0; i < _userList.Count; i++)
            {
                if (_userList[i].Name.Contains(Username) || _userList[i].Name == Username)
                {
                    Console.WriteLine("{0}", _userList[i].GetInfo());
                }
                
            }
            Console.WriteLine("Press any key to back to menu");
            Console.ReadKey();
            Console.Clear();
        }
        public bool IsIDExist(String id)
        {

            for (int i = 0; i < _userList.Count; i++)
            {
                if (_userList[i].ID == id)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
