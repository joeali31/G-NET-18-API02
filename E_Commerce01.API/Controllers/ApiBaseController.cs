using E_Commerce01.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce01.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        // Called if there is no value 
        public static ActionResult ToActionResult(Result result)
        {
            if (result.IsSuccess)
            {
                //return new OkObjectResult(result);
                return new OkResult();
            }

            return ToProblem(result.Errors);
        }

        // Called if there is value 
        public static ActionResult<TValue> ToActionResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Data);
            }

            return ToProblem(result.Errors);
        }

        protected static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var FirstError = errors[0];

            var statusCode = FirstError.ErrorType switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            var problems = new ProblemDetails() 
            {
                Detail = FirstError.Description,
                Title = FirstError.Code,
                Status = statusCode,
                Extensions = { ["Errors"] = errors}
            };

            return new ObjectResult(problems) { StatusCode = statusCode};
        }
    }
}
