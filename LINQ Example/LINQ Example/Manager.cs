using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Sql;
namespace LINQ_Example
{
    
    class Manager
    {

        private StudentModel _studentsDatabase;
        private List<Student> _studentList;

        public Manager(int numberOfStudent = Constants.DefaultNumberOfStudent)
        {
            _studentsDatabase = new StudentModel();
            _studentList = _studentsDatabase.GetAllStudents(numberOfStudent);
        }
        private void PrintStudentList(List<Student> students)
        {   
            int pageNumber = 1;
            int numberOfRecord = students.Count;
            int numberOfPage = numberOfRecord%Constants.MaxRecordPerPage == 0? numberOfRecord/Constants.MaxRecordPerPage:numberOfRecord/Constants.MaxRecordPerPage + 1;
           
            while (true)
            {
                Console.Clear();
                Console.WriteLine("LIST OF STUDENT:");
                Console.WriteLine("Total student: " + numberOfRecord+ "\n");
                var listOfStudentToShow = students.Skip((pageNumber - 1) * Constants.MaxRecordPerPage).Take(Constants.MaxRecordPerPage);
                int count = (pageNumber-1) * Constants.MaxRecordPerPage+1;
                foreach (Student student in listOfStudentToShow)
                {
                    Console.WriteLine(count++.ToString()+": "+student.ToString());
                }
                Console.WriteLine("\nCurrent page: " + pageNumber + "|This contains " + numberOfPage + " pages. Type page number to see more or type -1 to back:");
                Int32.TryParse(Console.ReadLine(), out pageNumber);
                if (pageNumber == -1)
                {
                    break;
                }
                if (pageNumber > numberOfPage || pageNumber < 1)
                {
                    pageNumber = 1;
                }
            }
        }
        public void ShowAll()
        {
            Console.Clear();
            PrintStudentList(_studentList.OrderBy(s=> s.StudentID).ToList<Student>());
        }
        public void Search()
        {
            Console.Clear();
            Console.WriteLine("Please enter a name or an id to search");
            string searchkeyWord = Console.ReadLine();
            var result = from student in _studentList
                         where student.StudentID.Contains(searchkeyWord) || (student.FirstName + " " + student.LastName).Contains(searchkeyWord)
                         orderby student.StudentID ascending
                         select student;
            Console.WriteLine("There are " + result.Count().ToString() + " student match the key word: " + searchkeyWord+". Press any key to show the list!");
            Console.ReadKey();
            PrintStudentList(result.ToList<Student>());

        }
        public void ShowClass()
        {
            if (_studentList.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("List empty. Press any key to back;");
                Console.ReadKey();
                return;
            }
            List<string> classNames = new List<string>();
            var classGroups = from student in _studentList
                            group student
                            by student.ClassID;
            foreach (var classGroup in classGroups)
            {
                classNames.Add(classGroup.Key);
            }
           
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose class to see student list");
                int count = 0;
                foreach (string className in classNames)
                {
                    Console.WriteLine(count++.ToString()+". "+className);
                }
                Console.WriteLine(count.ToString() + ". Exit.");
                int option = 0;
                Int32.TryParse(Console.ReadLine(), out option);
                if (option == count)
                {
                    break;
                }
                else if (option >= 0 && option < classNames.Count)
                {
                    var result = from student in _studentList
                                 where student.ClassID == classNames.ElementAt(option)
                                 select student;
                    PrintStudentList(result.ToList<Student>());
                }


            }

                           

                            
        }
        public void ShowTopStudents()
        {
            Console.Clear();

            Console.WriteLine("How many number of student to show?:");

            int number = 1;

            Int32.TryParse(Console.ReadLine(), out number);

            var result = _studentList.OrderByDescending(s => s.Gpa).Take(number).ToList<Student>();
            PrintStudentList(result);
        
        }
        public void Analyze()
        {
            Console.Clear();
            Console.WriteLine("Average GPA: " + Math.Round(_studentList.Average(s => s.Gpa),2));
            Console.WriteLine("Max GPA: " + _studentList.Max(s => s.Gpa));
            Console.WriteLine("Min GPA: " + _studentList.Min(s => s.Gpa));
            Console.WriteLine("Number of A student: " + _studentList.Where(s => s.Gpa >= 8.5).Count());
            Console.WriteLine("Number of B student: " + _studentList.Where(s => s.Gpa >= 7 && s.Gpa<8.5).Count());
            Console.WriteLine("Number of C student: " + _studentList.Where(s => s.Gpa >= 5.5&& s.Gpa<7).Count());
            Console.WriteLine("Number of D student: " + _studentList.Where(s => s.Gpa >= 4 && s.Gpa<5.5).Count());
            Console.WriteLine("Number of F student: " + _studentList.Where(s => s.Gpa <4).Count());
            Console.WriteLine("Press any key to back!");
            Console.ReadKey();
        }
        

    }

}
