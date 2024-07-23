using Communication.Responses;
using Configuration.Resources;
using ExceptionManager.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalog.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception.StackTrace);
            
        if (context.Exception is BaseException)
        {
            var exception = (BaseException)context.Exception;
            context.HttpContext.Response.StatusCode = (int)exception.GetStatusCode();

            var response = new ResponseErrorsJson(exception.GetErrorMessages());

            context.Result = new ObjectResult(response);
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new ResponseErrorsJson([ErrorMessagesResource.UNKNOWN_ERROR]);

            context.Result = new ObjectResult(response);
        }
    }
}
