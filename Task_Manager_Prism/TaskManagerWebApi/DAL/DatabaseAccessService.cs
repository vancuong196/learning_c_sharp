using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Configuration;
using Task_Manager_Prism.Models;

namespace Task_Manager_Prism.DatabaseAccess
{
    public class DatabaseAccessService: IDatabaseAccessService
    {

        private readonly string _connectionString;
        public DatabaseAccessService()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
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
                                    task.Time = reader.GetString(2).Trim();
                                    task.Date = reader.GetString(3).Trim();
                                    task.Title = reader.GetString(0).Trim();
                                    task.Description = reader.GetString(1).Trim();
                                    task.IsImportant = reader.GetBoolean(4);
                                    task.Tag = reader.GetString(5).Trim();
                                    task.ID = reader.GetInt32(6);
                                    task.IsFinished = reader.GetBoolean(7);
                                    Debug.WriteLine(task.Time + " " + task.Date + " " + task.ID.ToString() + " " + task.Title);
                                    tasks.Add(task);
                                 
                                }
                               

                            }
                        }
                    }
                }
                
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return tasks;
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
                            
                                    tag.Name = reader.GetString(0).Trim();
                                
                                    tags.Add(tag);
                                }
                            }
                        }
                    }
                }
                
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return tags;
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

        public void AddTaskItem(TaskItem item)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(
                        "INSERT INTO TasksTable (Title, Description,Time,Date,Priority,Tag,IsCompleted ) VALUES(@Title, @Description, @Time,@Date,@Priority,@Tag,@IsCompleted)", con))
                    {
                        command.Parameters.Add(new SqlParameter("Title", item.Title));
                        command.Parameters.Add(new SqlParameter("Description", item.Description));
                        command.Parameters.Add(new SqlParameter("Time", item.Time));
                        command.Parameters.Add(new SqlParameter("Date", item.Date));
                        command.Parameters.Add(new SqlParameter("Priority", item.IsImportant));
                        command.Parameters.Add(new SqlParameter("Tag", item.Tag));
                        command.Parameters.Add(new SqlParameter("IsCompleted", item.IsFinished));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                
                { 

                    Debug.WriteLine("Count not insert."+e.Message);
                    throw new Exception();
                }
            }
        }

        public void UpdateTaskItem(TaskItem item)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(
                        "Update TasksTable Set Title=@Title, Description=@Description,Time=@Time,Date=@Date,Priority=@Priority,Tag=@Tag,IsCompleted=@IsCompleted Where ID=@ID;", con))
                    {
                       // Debug.WriteLine("update---------------------");
                        command.Parameters.Add(new SqlParameter("Title", item.Title));
                        command.Parameters.Add(new SqlParameter("Description", item.Description));
                        command.Parameters.Add(new SqlParameter("Time", item.Time));
                        command.Parameters.Add(new SqlParameter("Date", item.Date));
                        command.Parameters.Add(new SqlParameter("Priority", item.IsImportant));
                        command.Parameters.Add(new SqlParameter("Tag", item.Tag));
                        command.Parameters.Add(new SqlParameter("IsCompleted", item.IsFinished));
                        command.Parameters.Add(new SqlParameter("ID", item.ID));
                        command.ExecuteNonQuery();
                        
                    }
                }
                catch (Exception e)

                {

                    Debug.WriteLine("Count not update." + e.Message);
                    throw new Exception();
                }
            }
        }

        public void DeleteTaskItem(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(
                        "Delete from TasksTable Where ID=@ID;", con))
                    {
                        command.Parameters.Add(new SqlParameter("ID", id));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)

                {

                    Debug.WriteLine("Count not insert." + e.Message);
                }
            }
        }

        public void AddTagItem(string tagName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(
                        "INSERT INTO TagTable (TagName) VALUES(@Name)", con))
                    {
                        command.Parameters.Add(new SqlParameter("Name", tagName));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)

                {

                    Debug.WriteLine("Count not insert." + e.Message);
                }
            }
        }
    }
}
