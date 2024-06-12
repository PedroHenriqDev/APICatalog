using APICatalog.Domain;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace APICatalog.Extensions
{
    static public class ApiExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app) 
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(!(contextFeature is null)) 
                    {
                        await context.Response.WriteAsync(new ErrorDetails() 
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Trace = contextFeature.Error.StackTrace,
                        }.ToString());
                    }
                });
            });
        }
    }
}
