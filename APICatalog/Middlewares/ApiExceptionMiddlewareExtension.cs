using Infrastructure.Domain;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace APICatalog.Extensions;

public static class ApiExceptionMiddlewareExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeatures is not null)
                {
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeatures.Error.Message,
                        Trace = contextFeatures.Error.StackTrace,
                    }.ToString());
                }
            });
        });
    }
}