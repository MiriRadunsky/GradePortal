using GradeDO;
using GradeDO.Exceptions;
using GradePortal.Modols;
using GradePortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradePortal.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class GradeManagerController : Controller
    {
       private IGradeManager _gradeManager;
        private IStudents _students;
        ILogger<GradeManagerController> _logger;
        public GradeManagerController(IStudents students, ILogger<GradeManagerController> logger ,IGradeManager gradeManager)
        {
            _students = students;
            _logger = logger;
            _gradeManager = gradeManager;
        }
        [HttpPost("uploadGrades")]

        public IActionResult AddGradesToStudents([FromBody] AddGradesRequest request)
        {
            if (request.M_Grade == null || request.ListId == null)
            {
                _logger.LogError("There was an error entering grades for students.\nOne of the inputs was NULL");
                throw new ArgumentNullException();
            }
            request.M_Grade.ForEach(grade => { grade.ExeNumber = request.ExeNumber; });
            List<Grade> list_grades = request.M_Grade.Select(g => g.Convert()).ToList();
            List<Student> list_students = request.ListId.Select(id => _students.GetStudent(id)).ToList();
            _students.AddGradesToStudents(list_students ,list_grades);
            _logger.LogDebug("Grades were entered for the students according to the number of exercises");
            return Ok(list_grades);

        }
        [HttpPut("SetGrade")]
        public IActionResult SetGradeStudent([FromQuery] string id,M_Grade grade) {

            _logger.LogInformation($"trying to edit a grade for a student with ID{id}");
            _students.AddGradeToStudent(id, grade.Convert());
            _logger.LogInformation($"The grade for student with ID {id} was successfully updated.");
            return Ok();

        }


        //כתוב לעשות פונקציה שמחזירה ציונים לתרגיל ספצפי לא היה לנו כזה פונקציה בשום מחלקה אז יצרנו אותה עכשיו מה עושים אם  אין מספר תרגיל כזה או נגיד אין ציונים עדין?

        [HttpGet("GradesSpecificExe")]
        public IActionResult GradesSpecificExe(int ExeNumber)
        {
            _logger.LogInformation($"Fetching grades for exercise: {ExeNumber}");
            var grades = _students.GetGradesByExerciseCode(ExeNumber);

            if (grades == null || grades.Count == 0)
            {
                _logger.LogWarning($"No grades found for exercise {ExeNumber}.");
                return NotFound(new { Message = $"No grades found for exercise {ExeNumber}." });
            }

            _logger.LogInformation($"Found grades for exercise {ExeNumber}. Returning results.");
            return Ok(grades);
        }



        [HttpGet("CalcFinalGrade")]
        public IActionResult CalcFinalGrade(string id)
        {
       
            _logger.LogInformation($"  Calc final grade for student with ID: {id}");
           var finalGrade= _gradeManager.CalculateFinalGrade(id);
            _logger.LogInformation($" The Calc final grade student with ID: {id} is {finalGrade}");
            return Ok(finalGrade);

        }

        [HttpGet("allDetailes")]
        public IActionResult StudentsDetailesAndFinalGrade()
        {
            _logger.LogInformation($"Students detailes and finalGrade");
           var detailes= _gradeManager.GetStudentsDetailesAndFinalGrade();
            return Ok(detailes);
        }





    }
}
