using GradeDO.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeDO
{
    public class Students :  IStudents
    {
        DataSource studentsList = new DataSource();
        public Students()
        {
            studentsList.Initialize();
        }

        public void AddStudent(Student student)
        {
            if (studentsList.Students.Any(stu => stu.ID == student.ID))
                throw new StudentAlradyExsistException(student.ID);
            student.Password = student.ID;
            studentsList.Students.Add(student);

        }

        public List<Student> GetAllStudents()
        {
            return studentsList.Students;
        }

        public Student GetStudent(string studentId)
        {
            Student studentA = studentsList.Students.FirstOrDefault(stu => stu.ID == studentId);
            if (studentA == null)
                throw new StudentNotExsistException(studentId);
            return studentA;
        }

        public void DeleteStudent(string studentId)
        {

            Student studentA = studentsList.Students.FirstOrDefault(stu => stu.ID == studentId);
            if (studentA == null)
            {
                throw new StudentNotExsistException(studentId);
            }
            else

            {
                studentsList.Students.Remove(studentA);

            }

        }

        public void SetStudent(Student student,string id)
        {
            Student s = studentsList.Students.FirstOrDefault(stu => stu.ID == student.ID);
            if (s!=null)
            {
                s.Name = student.Name;
                s.Password = student.Password;

            }
            else
            {
                throw new StudentNotExsistException(student.ID);
            }
        }

        public void DisplayStudent(Student student)
        {
            student.ToString();
        }

        public void DisplayStudents()
        {
            foreach (Student student in studentsList.Students) { student.ToString(); }
        }

        public void AddGradesToStudents(List<Student> studentsList, List<Grade> gradeList)
        {
            if (gradeList.Count != studentsList.Count)
                throw new ListsAreNotEqualInLenghException();
            for (int i = 0; i < studentsList.Count; i++)
            {
                AddGradeToStudent(studentsList[i].ID, gradeList[i]);
            }
        }


        public void AddGradeToStudent(string StudentId, Grade grade)
        {
            Student student = studentsList.Students.FirstOrDefault(stu => stu.ID == StudentId);
            if (student == null)
                throw new StudentNotExsistException(StudentId);
            grade.Date = DateTime.Today;
            student.ExeList.Add(grade);
        }

        public Grade ReturnGrade(string StudentId, int CodeExe)
        {
            Student student = studentsList.Students.FirstOrDefault(stu => stu.ID == StudentId);
            if (student == null)
                throw new StudentNotExsistException(StudentId);
            Grade grade = student.ExeList.FirstOrDefault(gr => gr.ExeNumber == CodeExe);
            if (grade == null)
                throw new GradeNotExsistException(CodeExe);
            return grade;
        }

        public int ReturnTestGrade(string StudentId)
        {
            Student student = studentsList.Students.FirstOrDefault(stu => stu.ID == StudentId);
            if (student == null)
                throw new StudentNotExsistException(StudentId);
            int grade = student.TestGrade.GradeNumber;
            if (grade == null)
                throw new GradeNotExsistException(99);
            return grade;
        }
        

        public List<Grade> GetGradesByExerciseCode(int codeExe)
        {

            List<Grade> GradeList = new List<Grade>();
            for (int i = 0; i < studentsList.Students.Count; i++)
            {
                var totalScore = ReturnGrade(studentsList.Students[i].ID, codeExe);
                if (totalScore != null)
                {
                    GradeList.Add(totalScore);
                }
            }
         
            return GradeList;
        }

        public int ReturnLastSubmission(string StudentId)
        {
            Student student = studentsList.Students.FirstOrDefault(stu => stu.ID == StudentId);
            return student.ExeList.Count;
        }
    }
}
