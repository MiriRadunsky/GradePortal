
namespace GradePortal.Services
{
    public interface IGradeManager
    {
        int AverageExe(int exeCode);
        double CalculateFinalGrade(string StudentId);
        List<string> GetStudentsDetailesAndFinalGrade();
    }
}