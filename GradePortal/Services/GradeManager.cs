using GradeDO;
using GradeDO.Exceptions;
using GradePortal.Configurations;
using GradePortal.Modols;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace GradePortal.Services
{
    public class GradeManager : IGradeManager
    {
        private IStudents _students;
        List<ExerciseService> _ExerciseService;
        public GradeManager(IOptions<List<ExerciseService>> exerciseService, IStudents students)
        {
        
            _students = students;
            _ExerciseService = exerciseService.Value;
        }
       

        public double CalculateFinalGrade(string id)
        {
            
            double finalGrade = 0;
            Student student = _students.GetStudent(id);
            if (student != null)
            {
                foreach (Grade grade in student.ExeList)
                {
                    finalGrade += (grade.GradeNumber * _ExerciseService.Find(p => p.ExeNumber == grade.ExeNumber).Percent) / 100;
                    
                }     
                finalGrade += student.TestGrade.GradeNumber * _ExerciseService.Find(p => p.ExeNumber == 99).Percent / 100;
                return finalGrade;
            }
            throw new StudentNotExsistException(id);

             }


        public List<string> GetStudentsDetailesAndFinalGrade()
        {
            List<string> studentsDetailes = new List<string>();
            foreach (Student s in _students.GetAllStudents())
            {
                studentsDetailes.Add($"{s.ToString()}, final grade: {CalculateFinalGrade(s.ID)}");
            }
            return studentsDetailes;
        }

        public int AverageExe(int exeCode)
        {
            int sum = 0;
            int count = 0;

            foreach (Student s in _students.GetAllStudents())
            {
                foreach (Grade g in s.ExeList)
                {
                    if (g.ExeNumber == exeCode)
                    {
                        sum += g.GradeNumber;
                        count++;
                        break;
                    }
                }
            }
            if (count == 0)
            {
                throw new Exception($"No grades found for exercise {exeCode}.");
            }

            return sum / count;
        }
    }
}