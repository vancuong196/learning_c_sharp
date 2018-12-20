using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Example
{
    class Student
    {
        public string StudentID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public int Age { set; get; }
        public string ClassID { set; get; }

        public double Gpa { set; get; }

        public Student()
        {
          
        }
        public Student(string studentID, string firstName, string lastName, int age, string classID, double gpa)
        {
            this.StudentID = studentID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.ClassID = classID;
            this.Gpa = gpa;
        }
        public override string ToString()
        {
            return ("ID: " + StudentID + "; Name: " + FirstName + " " + LastName + "; Age: " + Age + "; Class: " + ClassID + "; GPA:" + Gpa);
        }

    }
}
