using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskmanager.Models;

namespace Taskmanager.DatabaseAccess
{
    public class DatabaseAccessService: IDatabaseAccessService
    {

        private string _connectionString =
        @"Data source=CPU003;initial catalog=TaskDatabase;user id=sa;password=123456;MultipleActiveResultSets=True;";
        private List<TaskItem> LoadTasksInDatabase()
        {
            string query = @"SELECT * FROM TasksTable;";
            var tasks = new List<TaskItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
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
                                    task.Time = reader.GetString(2);
                                    task.Date = reader.GetString(3);
                                    task.Title = reader.GetString(0);
                                    task.Description = reader.GetString(1);
                                    task.IsImportant = reader.GetBoolean(4);
                                    task.Tag = reader.GetString(5);
                                    task.ID = reader.GetInt32(6);
                                    task.IsFinished = reader.GetBoolean(7);
                                    Debug.WriteLine(task.Time + " " + task.Date + " " + task.ID.ToString() + " " + task.Title);
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
        private List<TagItem> LoadTagInDatabase()
        {
            string query = @"select * from TagTable;";
            var tags = new List<TagItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
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
                                    var tag = new TagItem();
                            
                                    tag.Name = reader.GetString(0);
                                
                                    tags.Add(tag);
                                }
                            }
                        }
                    }
                }
                return tags;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        public async Task<List<TaskItem>> GetTasks()
        {
            List<TaskItem> tasks = LoadTasksInDatabase();
            return tasks;
        }

        public async Task<List<TagItem>> GetTags()
        {
            return LoadTagInDatabase();
        }

        public void SaveTaskItem(TaskItem item)
        {
            
        }
    }
}
