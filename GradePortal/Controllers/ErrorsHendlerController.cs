using GradeDO.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GradePortal.Controllers
{

    [Route("[Controller]")]
    [ApiController]
    public class ErrorsHendlerController : Controller
    {

        private readonly ILogger _logger;

        public ErrorsHendlerController(ILogger<ErrorsHendlerController> logger)
        {
            _logger = logger;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]

        public IActionResult HandleError()
        {

            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionDetails != null)
            {
                _logger.LogError(exceptionDetails.Error.Message, "error was throwed");
                _logger.LogDebug(exceptionDetails.Error, "");
            }

            if (exceptionDetails?.Error is StudentNotExsistException stuNot)
            {
                _logger.LogWarning("Student not exsist");
                return BadRequest( Problem(detail: exceptionDetails?.Error.Message,
                title: "Student does not exist",
                statusCode: stuNot.StatusCode));
                
            }

            if (exceptionDetails?.Error is StudentAlradyExsistException stuExe)
            {
                _logger.LogWarning("Student alrady exsist");
                return BadRequest(Problem(detail: exceptionDetails?.Error.Message,
                title: "Student alrady exsist",
                statusCode: stuExe.StatusCode));

            }


            if (exceptionDetails?.Error is GradeNotExsistException graExe)
            {
                _logger.LogWarning("Grade not exist");

                return BadRequest(Problem(detail: exceptionDetails?.Error.Message,
                    title: "Grade does not exist",
                    statusCode: graExe.StatusCode ));

            }



            if (exceptionDetails?.Error is ListsAreNotEqualInLenghException lisExe)
            {
                _logger.LogWarning("The number of grades must match the number of students.");
                return BadRequest(Problem(detail: exceptionDetails?.Error.Message,
                  title: "Lists are not of equal length",
                  statusCode: lisExe.StatusCode));
             
            }

            if (exceptionDetails?.Error is NullReferenceException)
            {
                return BadRequest(Problem(
                detail: "Please connet the owner of the website 0507777777",
                title: "An error occurred",
                statusCode: 870
                ));

            }

            return Problem(
             detail: "Please refresh the website",
             title: "An error occurred",
             statusCode: 501
         );
        }

}
}
