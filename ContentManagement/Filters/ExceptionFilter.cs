using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ContentManagement.Filters;

public class ExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// Handles exceptions that occur during the execution of the action.
    /// </summary>
    /// <param name="context">The context of the exception, including the exception itself and the result to be returned.</param>
    public void OnException(ExceptionContext context)
    {
        // Set a custom error message and HTTP status code for the response
        context.Result = new ObjectResult("An error occurred: " + context.Exception.Message)
        {
            StatusCode = 500 // Internal Server Error
        };
    }
}
