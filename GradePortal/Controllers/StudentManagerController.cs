using GradeDO;
using GradePortal.Modols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GradePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentManagerController : Controller
    {
        private IStudents _students;
        ILogger<StudentManagerController> _logger;
        public StudentManagerController(IStudents students,ILogger<StudentManagerController> logger)
        {
            _students=students;
            _logger =logger;
        }
        [HttpPost("AddStudent")]
    
        public IActionResult AddStudent([FromQuery] M_Student student)
        {
            if (student == null)
            {
                _logger.LogInformation($"didn't manage to add a student - a null student");

                throw new ArgumentNullException(nameof(student));
            }


            if (student.Convert() == null)
            {
                _logger.LogInformation($"didn't manage to add a student a problem in converting");
                return BadRequest();
            }
            else
            {
                _students.AddStudent(student.Convert());
                _logger.LogInformation($"added a student with ID{student.ID}");
                return Ok();
            }
        }

        [HttpPut("Delete")]
        public IActionResult DeletStudent(string studentId)
        {
            _students.DeleteStudent(studentId);
            _logger.LogInformation($" deleted a student with id{studentId}");
            return Ok();

        }

        [HttpPut("EditStudent")]
        public IActionResult EditStudent( string id,[FromQuery] M_Student student)
        {
            if (student == null)
            {
                _logger.LogError($"didn't manage to edit student - a null student");

                throw new ArgumentNullException(nameof(student));
            }


            if (student.Convert() == null)
            {
                _logger.LogError($"didn't manage to edit a student a problem in converting");
                return BadRequest();
            }
            else
            {
                _students.SetStudent(student.Convert() ,id);
                _logger.LogInformation($"edited a student with ID{student.ID}");
                return Ok();
            }

        }

        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            _logger.LogInformation("display all students");
            var students=_students.GetAllStudents();
            return Ok(students);
        }


        [HttpGet("GetStudent")]
        public IActionResult GetStudent(string studentId)
        {
            _logger.LogInformation($"display a student with ID{studentId}");
            var student=_students.GetStudent(studentId);
            return Ok(student);
        }





    }
}
