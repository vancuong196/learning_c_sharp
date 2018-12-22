using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using Taskmanager.Models;


namespace Taskmanager.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<TaskItem> _allTask;

        public ObservableCollection<TaskItem> AllTasks
        {
            get { return _allTask; }
            set { Set(ref _allTask, value); }
        }

        private TaskItem _selectedTaskItem;

        public TaskItem SelectedFeedItem
        {
            get
            {
                return _selectedTaskItem;
            }
            set
            {
                Set(ref _selectedTaskItem, value);
            }
        }
        public MainViewModel()
        {
            var taskList = new ObservableCollection<TaskItem>();
            taskList.Add(new TaskItem("Test1", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem("Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));

            _allTask = taskList;
            Load();
        }

        string connectionString =
            "Data Source=CPU003;" +
            "Initial Catalog=TaskDatabase;" +
            "User id=sa;" +
            "Password=123456;";
        public ObservableCollection<TaskItem> Load()
        { 
            string query = "select * from TaskTable;";
            var tasks = new ObservableCollection<TaskItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = query;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var task = new TaskItem();
                                    task.Time = reader.GetString(3);
                                    task.Date = reader.GetString(2);
                                    task.Title = reader.GetString(0);
                                    task.Description = reader.GetString(1);
                                    task.IsImportant = reader.GetBoolean(4);
                                    task.Tag = reader.GetString(5);
                                    tasks.Add(task);
                                }
                            }
                        }
                    }
                }
                return tasks;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }
}

        
    
