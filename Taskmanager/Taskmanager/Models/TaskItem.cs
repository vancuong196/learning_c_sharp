using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskmanager.Models
{


    public class TaskItem
    {
        public string Title { set; get; }
        public string Description { set; get; }
        public string Time { set; get; }
        public string Date { set; get; }
        public string Tag { set; get; }
        public bool IsImportant { set; get; }
        public TaskItem()
        {

        }
        public TaskItem(string title, string time, string date, string description, bool isImportant,string tag)
        {
            Title = title;
            Time = time;
            Description = description;
            Date = date;
            Time = time;
            IsImportant = isImportant;
            Tag = tag;
        }


    }
}
