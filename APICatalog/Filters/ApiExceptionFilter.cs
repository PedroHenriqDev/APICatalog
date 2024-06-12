using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalog.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger) 
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An error ocurred in handler exception");

            context.Result = new ObjectResult("The request generated an internal error") 
            {
                StatusCode = 500,
            };
        }
    }
}
