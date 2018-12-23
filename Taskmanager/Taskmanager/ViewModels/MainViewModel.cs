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
        private ObservableCollection<TaskItem> _unFinishedTasks;
        private ObservableCollection<Tag> _allTags;

        public ObservableCollection<TaskItem> AllTasks
        {
            get { return _allTask; }
            set { Set(ref _allTask, value); }
        }
        
        public ObservableCollection<Tag> AllTag
        {
            get { return _allTags; }
            set { Set(ref _allTags, value); }
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
            var tagList = new ObservableCollection<Tag>();
            tagList.Add(new Tag("Todo"));
            tagList.Add(new Tag("Must do"));
            tagList.Add(new Tag("Remind"));
            _allTags = tagList;

            _allTask = taskList;
            //Load();
        }

        string connectionString =
            "Data Source=ACE;" +
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

        
    
