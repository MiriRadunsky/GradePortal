using GradeDO;
using GradePortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace GradePortal.Controllers
{

    [Route("[Controller]")]
    [ApiController]
    public class ViewGradesController : Controller
    {
        private IGradeManager _gradeManager;
        private IStudents _students;
        private IPasswordManager _passwordManager;
        ILogger<ViewGradesController> _logger;
        public ViewGradesController(IStudents students, ILogger<ViewGradesController> logger, IGradeManager gradeManager , IPasswordManager passwordManager)
        {
            _students = students;
            _logger = logger;
            _gradeManager = gradeManager;
            _passwordManager = passwordManager;

        }

        [HttpGet("LastSubmission")]
        public IActionResult LastSubmission([FromQuery] string id, [FromQuery] string password)
        {
            _logger.LogInformation($"The student with ID number {id} is trying to log into the system");
            bool b=_passwordManager.ValidatePassword(id, password);
            if(!b)
            {
                _logger.LogInformation("Access Denied");
                BadRequest("Access Denied");
            }
            _logger.LogInformation($"The student with id {id} logged into the system");
           
            Grade grade = _students.ReturnGrade(id, _students.ReturnLastSubmission(id));
            _logger.LogInformation($"The student with the ID {id} number is requesting her latest grade.,her grade is{grade.GradeNumber}");
            return Ok(grade);
        }
        [HttpGet("GetStudentGrade")]
        public IActionResult GetStudentGrade([FromQuery] string id, [FromQuery] string password, [FromQuery] int codeExe)
        {
            _logger.LogInformation($"The student with ID number {id} is trying to log into the system");
            bool b = _passwordManager.ValidatePassword(id, password);
            if (!b)
            {
                _logger.LogInformation("Access Denied");
                BadRequest("Access Denied");
            }
            _logger.LogInformation($"The student with id {id} logged into the system");
            _logger.LogInformation($"The student with the ID number{id} is trying to retrieve her grade based on the course code.");
            Grade grade = _students.ReturnGrade(id,codeExe);
            _logger.LogInformation($"The student with ID{id} got{grade.GradeNumber} in the exe{codeExe}");
            return Ok(grade);
        }
        [HttpGet("GetStudentGradeTest")]
        public IActionResult GetStudentGradeTest([FromQuery] string id, [FromQuery] string password)
        {
            _logger.LogInformation($"The student with ID number {id} is trying to log into the system");
            bool b = _passwordManager.ValidatePassword(id, password);
            if (!b)
            {
                _logger.LogInformation("Access Denied");
                BadRequest("Access Denied");
            }
            _logger.LogInformation($"The student with id {id} logged into the system");
            _logger.LogInformation($"The student with the ID number{id} is trying to retrieve her grade in the test.");
            int grade = _students.ReturnTestGrade(id);
            _logger.LogInformation($"The student with ID{id} got{grade} in the test");
            return Ok(grade);
        }
        [HttpGet("GetStudentFanialGrade")]
        public IActionResult GetStudentFanialGrade([FromQuery] string id, [FromQuery] string password)
        {
            _logger.LogInformation($"The student with ID number {id} is trying to log into the system");
             _passwordManager.ValidatePassword(id, password);
            if (_passwordManager.ValidatePassword(id, password))
            {
                _logger.LogInformation($"The student with id {id} logged into the system");
                _logger.LogInformation($"The student with the ID number{id} is trying to retrieve her final grade.");
                double grade = _gradeManager.CalculateFinalGrade(id);
                _logger.LogInformation($"The final grade for the student with ID {id} is {grade}.");
                return Ok(grade);
            }
       
            _logger.LogError("Access Denied");
            return NotFound("Wrong Password!");
        }














    }
}
