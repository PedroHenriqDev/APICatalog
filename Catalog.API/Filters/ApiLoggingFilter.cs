using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.API.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;
    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger) 
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation($"Executing action in {context.Controller.ToString()} -> Date: {DateTime.Now.ToLongTimeString()} - Model State: {context.ModelState.IsValid}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation($"Executed action in {context.Controller.ToString()} -> Date: {DateTime.Now.ToLongTimeString()} - Status Code: {context.HttpContext.Response.StatusCode}");
    }
}
