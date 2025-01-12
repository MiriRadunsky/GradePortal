
namespace GradeDO
{
    public interface IStudents
    {
        void AddGradesToStudents(List<Student> studentsList, List<Grade> gradeList);
        void AddGradeToStudent(string StudentId, Grade grade);
        void AddStudent(Student student);
        void DeleteStudent(string studentId);
        void DisplayStudent(Student student);
        void DisplayStudents();
        List<Student> GetAllStudents();
        List<Grade> GetGradesByExerciseCode(int codeExe);
        Student GetStudent(string studentId);
        Grade ReturnGrade(string StudentId, int CodeExe);
        void SetStudent(Student student,string id);
        public int ReturnLastSubmission(string StudentId);
        public int ReturnTestGrade(string StudentId);
    }
}