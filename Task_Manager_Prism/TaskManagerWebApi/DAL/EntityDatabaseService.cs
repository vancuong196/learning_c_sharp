using System;
using System.Collections.Generic;
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
        public void AddTagItem(string tagName)
        {
            using (var db = new TaskDatabaseContext())
            {
                TagTable tag = new TagTable();
                tag.TagName = tagName;
                db.TagTables.Add(tag);
                db.SaveChanges();
            }
        }

        public void AddTaskItem(TaskItem t)
        {

            using (var db = new TaskDatabaseContext())
            {
                TasksTable taskItem = new TasksTable();
                taskItem.Date = t.Date.Trim();
                taskItem.ID = t.ID;
                taskItem.Priority = t.IsImportant;
                taskItem.Title = t.Title;
                taskItem.Description = t.Description;
                taskItem.IsCompleted = t.IsFinished;
                taskItem.Tag = t.Tag.Trim();
                taskItem.Time = t.Time.Trim();
                db.TasksTables.Add(taskItem);
                db.SaveChanges();
            }
        }

        public void DeleteTaskItem(int id)
        {
          
            using (var db = new TaskDatabaseContext())
            {
                var task = db.TasksTables.Where(s => s.ID == id).Single();
                db.TasksTables.Remove(task);
                db.SaveChanges();
            }
          
        }

        public Task<List<TagItem>> GetTags()
        {
            List<TagItem> tagItems = new List<TagItem>();
            using (var db = new TaskDatabaseContext())
            {
                var tasks = db.TagTables.Where(s => true);
                foreach (TagTable t in tasks)
                {
                    TagItem tagItem = new TagItem();
                    tagItem.Name = t.TagName;
                    tagItems.Add(tagItem);

                }
            }
            return Task.FromResult(tagItems);
        }

        public Task<List<TaskItem>> GetTasks()
        {
            List<TaskItem> taskItems = new List<TaskItem>();
            using (var db = new TaskDatabaseContext())
            {
                var tasks = db.TasksTables.Where(s => true);
                foreach (TasksTable t in tasks)
                {
                    TaskItem taskItem = new TaskItem();
                    taskItem.Date = t.Date.Trim();
                    taskItem.ID = t.ID;
                    taskItem.IsImportant = t.Priority??false;
                    taskItem.Title = t.Title;
                    taskItem.Description = t.Description;
                    taskItem.IsFinished = t.IsCompleted??false;
                    taskItem.Tag = t.Tag.Trim();
                    taskItem.Time = t.Time.Trim();
                    taskItems.Add(taskItem);
                    
                }
            }
            return Task.FromResult(taskItems);
        }

        public void UpdateTaskItem(TaskItem t)
        {
           
            using (var db = new TaskDatabaseContext())
            {
                var task = db.TasksTables.Where(s => s.ID == t.ID).Single();
                if (task == null)
                {
                    return;
                }

                task.Date = t.Date.Trim();
                task.ID = t.ID;
                task.Priority = t.IsImportant;
                task.Title = t.Title;
                task.Description = t.Description;
                task.IsCompleted = t.IsFinished;
                task.Tag = t.Tag.Trim();
                task.Time = t.Time.Trim();
                db.SaveChanges();
            }
        }
    }
}