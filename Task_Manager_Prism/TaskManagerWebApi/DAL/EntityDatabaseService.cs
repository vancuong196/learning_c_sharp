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
        public bool AddTagItem(string tagName)
        {
            try
            {
                using (var db = new Entities())
                {
                    TagTable tag = new TagTable();
                    tag.TagName = tagName;
                    db.TagTables.Add(tag);
                    db.SaveChanges();
                    db.Dispose();
                    return true;

                }
            } catch (Exception e)
            {
               
                return false;
            }
        }

        public bool AddTaskItem(TaskItem t)
        {
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
            try
            {
                using (var db = new Entities())
                {
                    var task = db.TasksTables.Where(s => s.ID == id).Single();
                    db.TasksTables.Remove(task);
                    db.SaveChanges();
                    db.Dispose();
                    return true;

                }
            } catch
            {
               
                return false;
            }
          
        }

        public bool FindTaskById(int id)
        {
            try
            {
                using (var db = new Entities())
                {
                    var task = db.TasksTables.Where(s => s.ID == id).Single();
                    if (task == null)
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
            try
            {
                List<TagItem> tagItems = new List<TagItem>();
                using (var db = new Entities())
                {
                    var tasks = db.TagTables.Where(s => true);
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
            try
            {
                List<TaskItem> taskItems = new List<TaskItem>();
                using (var db = new Entities())
                {
                    var tasks = db.TasksTables.Where(s => true);
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

        public bool UpdateTaskItem(TaskItem t)
        {
            try
            {
                using (var db = new Entities())
                {
                    var task = db.TasksTables.Where(s => s.ID == t.ID).Single();
                    if (task == null)
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