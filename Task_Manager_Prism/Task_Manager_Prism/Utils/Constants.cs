﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager_Prism.Ultils
{
    public class Constants
    {
        public const int AllTaskListID = 0;
        public const int OverdueTaskListID = 1;
        public const int ImportantTaskListID = 2;
        public const int FinishedTaskListID = 3;
        public const int TodayTaskListID = 4;
        public const int NormalTaskListID = 5;
        public const int NoneDateTaskListID = 6;
        public const string SelectedTaskKey = "SELECTEDTASK";
        public const string TasksListKey = "TASKLIST";
        public const string ApiBaseUrl = "http://localhost:54784/api/";
        public const string LoginBaseUrl = "http://localhost:54784/token";

    }
}
