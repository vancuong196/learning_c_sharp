using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskmanager.Models;

namespace Taskmanager.DatabaseAccess
{
    public class FakeDatabaseAccessService : IDatabaseAccessService
    {
        public Task<List<TaskItem>> GetTasks()
        {
            List<TaskItem> taskList = new List<TaskItem>();
            taskList.Add(new TaskItem(1,"Test1", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem(2,"Test2", "12:30", "10/10/2018", "Descrition1", false, "Todo"));
            taskList.Add(new TaskItem(3,"Test3", "12:30", "25/12/2018", "Descrition12", true, "Todo"));
            taskList.Add(new TaskItem(5,"Test4", "12:30", "21/11/2018", "Descrition13", true, "Todo",true));
            taskList.Add(new TaskItem(6,"Test5", "12:30", "24/12/2018", "Descrition14", true, "Todo"));
            taskList.Add(new TaskItem(7,"Test6", "12:30", "24/11/2018", "Descrition15", false, "Todo",true));
            taskList.Add(new TaskItem(8,"Test7", "12:30", "24/10/2018", "Descrition16", true, "Todo"));
            taskList.Add(new TaskItem(9,"Test8", "12:30", "24/09/2018", "Descrition17", true, "Todo"));
            taskList.Add(new TaskItem(10,"Test9", "12:30", "24/12/2018", "Descrition18", true, "Todo"));
            taskList.Add(new TaskItem(10,"Test9", "", "", "Descrition18", true, "Todo"));
            taskList.Add(new TaskItem(10,"Test9", "", "", "Descrition18", true, "Todo"));


            return Task.FromResult(taskList);
            
        }
        public Task<List<TagItem>> GetTags()
        {
            var tagList = new List<TagItem>();
            tagList.Add(new TagItem("Todo"));
            tagList.Add(new TagItem("Must do"));
            tagList.Add(new TagItem("Remind"));
            return Task.FromResult(tagList);
        }

        public void AddTaskItem(TaskItem item)
        {
            throw new NotImplementedException();
        }

        public void UpdateTaskItem(TaskItem item)
        {
            throw new NotImplementedException();
        }

        public void DeleteTaskItem(int id)
        {
            throw new NotImplementedException();
        }

        public void AddTagItem(string tagName)
        {
            throw new NotImplementedException();
        }
    }
}
