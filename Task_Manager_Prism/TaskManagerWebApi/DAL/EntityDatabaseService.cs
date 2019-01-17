using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Task_Manager_Prism.DatabaseAccess;
using Task_Manager_Prism.Models;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.DAL
{
    public class EntityDatabaseService : IDatabaseAccessService
    {
        private string _userID;
        public bool AddTagItem(string tagName)
        {
            if (_userID == null)
            {
                Debug.WriteLine("UserID is not set");
                return false;
            }
            try
            {
                using (var db = new Entities())
                {
                    TagTable tag = new TagTable();
                    tag.TagName = tagName;
                    tag.UserID = _userID;
                    db.TagTables.Add(tag);
                    db.SaveChanges();
                    db.Dispose();
                    return true;

                }
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool AddTaskItem(TaskItem t)
        {
            if (_userID == null)
            {
                Debug.WriteLine("UserID is not set");
                return false;
            }
            try
            {
                using (var db = new Entities())
                {
                    TasksTable taskItem = new TasksTable();
                    taskItem.Date = t.Date.Trim();
                    taskItem.ID = t.ID;
                    taskItem.Priority = t.IsImportant;
                    taskItem.Title = t.Title.Trim();
                    taskItem.Description = t.Description.Trim();
                    taskItem.IsCompleted = t.IsFinished;
                    taskItem.Tag = t.Tag.Trim();
                    taskItem.Time = t.Time.Trim();
                    taskItem.UserId = _userID;
                    db.TasksTables.Add(taskItem);
                    db.SaveChanges();
                    db.Dispose();
                    return true;
                }
            } catch (Exception e)
            {
               
                return false;
            }
        }

        public bool DeleteTaskItem(int id)
        {
            if (_userID == null)
            {
                Debug.WriteLine("UserID is not set");
                return false;
            }
            try
            {
                using (var db = new Entities())
                {
                    var task = db.TasksTables.Where(s => s.ID == id).Single();
                    if (task.UserId.Trim() == _userID.Trim())
                    {
                        db.TasksTables.Remove(task);
                        db.SaveChanges();
                        db.Dispose();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            } catch
            {
               
                return false;
            }
          
        }

        public bool FindTagByName(string name)
        {
            if (_userID == null)
            {
                Debug.WriteLine("UserID is not set");
                return true;
            }
            try
            {
                using (var db = new Entities())
                {
                    var tag = db.TagTables.Where(s => s.TagName.Trim() == name && s.UserID.Trim() == _userID).Single();
                    if (tag == null)
                    {
                        db.Dispose();
                        return false;
                    }
                    db.Dispose();
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }

        public bool FindTaskById(int id)
        {
            if (_userID == null)
            {
                Debug.WriteLine("UserID is not set");
                return false;
            }
            try
            {
                using (var db = new Entities())
                {
                    var task = db.TasksTables.Where(s => s.ID == id).Single();
                    if (task == null|| task.UserId.Trim() != _userID)
                    {
                        db.Dispose();
                        return false;
                    }
                    db.Dispose();
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        public Task<List<TagItem>> GetTags()
        {
            if (_userID == null)
            {
                Debug.WriteLine("UserID is not set");
                return Task.FromResult(new List<TagItem>());
            }
            try
            {
                Debug.WriteLine("Getting all tags");
                List<TagItem> tagItems = new List<TagItem>();
                using (var db = new Entities())
                {
                    var tasks = db.TagTables.Where(s => s.UserID.Trim().Equals(_userID.Trim()));
                    foreach (TagTable t in tasks)
                    {
                        TagItem tagItem = new TagItem();
                        tagItem.Name = t.TagName.Trim();
                        tagItems.Add(tagItem);

                    }
                    db.Dispose();
                }
                return Task.FromResult(tagItems);
            } catch
            {
              
                return null;
            }
        }

        public Task<List<TaskItem>> GetTasks()
        {
            if (_userID == null)
            {
                Debug.WriteLine("UserID is not set");
                return Task.FromResult(new List<TaskItem>());
            }
            try
            {
                List<TaskItem> taskItems = new List<TaskItem>();
                using (var db = new Entities())
                {
                    var tasks = db.TasksTables.Where(s => s.UserId == _userID);
                    foreach (TasksTable t in tasks)
                    {
                        TaskItem taskItem = new TaskItem();
                        taskItem.Date = t.Date.Trim();
                        taskItem.ID = t.ID;
                        taskItem.IsImportant = t.Priority ?? false;
                        taskItem.Title = t.Title.Trim();
                        taskItem.Description = t.Description.Trim();
                        taskItem.IsFinished = t.IsCompleted ?? false;
                        taskItem.Tag = t.Tag.Trim();
                        taskItem.Time = t.Time.Trim();
                        taskItems.Add(taskItem);
                        
                    }
                    db.Dispose();
                }
                return Task.FromResult(taskItems);
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
              
                return null;
            }
        }

        public void SetCurrentUser(string userID)
        {
            _userID = userID;
        }

        public bool UpdateTaskItem(TaskItem t)
        {
            if (_userID == null)
            {
                Debug.WriteLine("UserID is not set");
                return false;
            }
            try
            {
                using (var db = new Entities())
                {
                    var task = db.TasksTables.Where(s => s.ID == t.ID).Single();
                    if (task == null || task.UserId.Trim() != _userID)
                    {
                        return false;
                    }
                    task.Date = t.Date.Trim();
                    task.ID = t.ID;
                    task.Priority = t.IsImportant;
                    task.Title = t.Title.Trim();
                    task.Description = t.Description.Trim();
                    task.IsCompleted = t.IsFinished;
                    task.Tag = t.Tag.Trim();
                    task.Time = t.Time.Trim();
                    db.SaveChanges();
                    db.Dispose();
                    return true;
                }
            } catch
            {
             
                return false;
            }

        }

    }
}