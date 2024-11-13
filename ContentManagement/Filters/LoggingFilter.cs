using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ContentManagement.Filters;

/// <summary>
/// A filter for logging information about action execution in the application.
/// Logs the start and end of actions, along with the execution duration.
/// </summary>
public class LoggingFilter : IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;
    private Stopwatch? timer;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingFilter"/> class.
    /// </summary>
    /// <param name="logger">The logger instance used for logging information.</param>
    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Called before the action method is executed.
    /// Starts a stopwatch timer and logs the name of the executing action.
    /// </summary>
    /// <param name="context">The context of the action being executed.</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        timer = Stopwatch.StartNew();
        _logger.LogInformation($"Executing action: {context.ActionDescriptor.DisplayName}");
    }

    /// <summary>
    /// Called after the action method has executed.
    /// Stops the timer and logs the name and duration of the executed action.
    /// </summary>
    /// <param name="context">The context of the executed action.</param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        timer?.Stop();
        _logger.LogInformation($"Executed action: {context.ActionDescriptor.DisplayName}");
        _logger.LogInformation($"Executed action time: {timer?.Elapsed.TotalMilliseconds} ms");
    }
}
