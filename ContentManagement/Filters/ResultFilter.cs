using Microsoft.AspNetCore.Mvc.Filters;

namespace ContentManagement.Filters;

public class ResultFilter : IResultFilter
{
    private readonly ILogger<ResultFilter> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultFilter"/> class.
    /// </summary>
    /// <param name="logger">The logger used for logging information during result processing.</param>
    public ResultFilter(ILogger<ResultFilter> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Called before the action result is executed. It allows modifying the response before it is sent to the client.
    /// </summary>
    /// <param name="context">The context of the result that is being executed.</param>
    public void OnResultExecuting(ResultExecutingContext context)
    {
        // Adding a custom header to the response
        context.HttpContext.Response.Headers.Add("Custom-Header", "This is a custom header");

        // Conditionally adding a header based on the HTTP request method
        if (context.HttpContext.Request.Method == "GET")
        {
            context.HttpContext.Response.Headers.Add("X-Request-Type", "GET Request");
        }
        else
        {
            context.HttpContext.Response.Headers.Add("X-Request-Type", "Non-GET Request");
        }
    }

    /// <summary>
    /// Called after the action result has been executed. It allows logging and other post-processing of the response.
    /// </summary>
    /// <param name="context">The context of the executed result.</param>
    public void OnResultExecuted(ResultExecutedContext context)
    {
        // Log a message indicating that the response has been processed
        _logger.LogInformation("Response processing completed for {Path}", context.HttpContext.Request.Path);

        // Log the status code of the response
        _logger.LogInformation("Response status code: {StatusCode}", context.HttpContext.Response.StatusCode);
    }
}
