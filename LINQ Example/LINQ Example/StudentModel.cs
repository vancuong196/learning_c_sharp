
using System;
using System.Collections.Generic;
using System.Threading;

namespace LINQ_Example
{
    class StudentModel
    {
        private static Random _numberGenerator = new Random();


        private Student GenerateStudent()
        {
            string firstName = Constants.FirstName[_numberGenerator.Next(0, Constants.FirstName.Length)];
           
            string midleName = Constants.MidleName[_numberGenerator.Next(0, Constants.MidleName.Length)];
        
            string name = Constants.LastName[_numberGenerator.Next(0, Constants.LastName.Length)];
           
            string classId = Constants.ClassID[_numberGenerator.Next(0, Constants.ClassID.Length)];

            string studentID = _numberGenerator.Next(10000, 99999).ToString();

            int age = _numberGenerator.Next(18, 25);

            double gpa = GetRandomDouble(1.0,10.0);

            return new Student(studentID, firstName, midleName +" "+ name, age, classId, gpa);
        }
        private double GetRandomDouble(double minimum, double maximum)
        {
            
            return Math.Round(_numberGenerator.NextDouble() * (maximum - minimum) + minimum,3);
        }

        public List<Student> GetAllStudents(int numberOfStudent)
        {
            List<Student> students = new List<Student>();
            while (numberOfStudent-- > 0)
            {
                Student student = GenerateStudent();
                students.Add(student);
                
            }
            return students;
        }


    }
}
        
