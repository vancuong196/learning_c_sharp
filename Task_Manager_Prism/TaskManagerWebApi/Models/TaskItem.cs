using System;
using System.Globalization;


namespace Task_Manager_Prism.Models
{


    public class TaskItem
    {

        private string _time;
        private string _date;
        public string Title { set; get; }

        public string Description { set; get; }

        public string Time
        {
            set
            {
                _time = value.Trim();
            }
            get
            {
                if (_time == "" || _time == null)
                {
                    return "--:--:--";
                }
                return _time;
            }
        }
        public string Date
        {
            set
            {
                _date = value.Trim();
            }
            get
            {
                if (_date == null || _date == "")
                {
                    return "--/--/----";
                }
                return _date;
            }
        }

        public string Tag { set; get; }

        public bool IsImportant { set; get; }

        public int ID { set; get; }

        public bool IsFinished { set; get; }

        public bool IsUnFinish
        {
            get
            {
                return !IsFinished;
            }
        }
        public bool IsNormal
        {
            get
            {
                return !IsImportant;
            }
        }
        public bool IsTimeConstraint
        {
            get
            {
                if (Date.Trim() == "" || Time.Trim() == "" || Date == "--/--/----" || Time == "--:--:--")
                {
                    return false;
                }
                return true;

            }

        }

        public DateTimeOffset DateType
        {
            get
            {
                if (IsTimeConstraint)
                {
                    return DateTime.ParseExact(Date, "MM/dd/yyyy",
                            CultureInfo.InvariantCulture);
                }
                return DateTime.Today;
            }
        }

        public string Priority
        {
            get
            {
                return IsImportant == true ? "Important" : "Normal";
            }
        }

        public TaskItem(int id, string title, string time, string date, string description, bool isImportant, string tag, bool isFinished = false)
        {
            ID = id;
            Title = title;
            Time = time;
            IsFinished = isFinished;
            Description = description;
            Date = date;
            Time = time;
            IsImportant = isImportant;
            Tag = tag;
        }

        public TaskItem()
        {

        }

        public override string ToString()
        {
            return Title + " " + Description + " " + Date + " " + Time + " " + IsImportant + " " + IsFinished;
        }



    }
}
