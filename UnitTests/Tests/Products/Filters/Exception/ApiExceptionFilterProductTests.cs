using APICatalog.Controllers;
using APICatalog.Filters;
using Communication.DTOs.Responses;
using ExceptionManager.ExceptionBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Tests.Products.Filters.Exception;

public class ApiExceptionFilterProductTests : IClassFixture<ProductsControllerUnitTest>
{
    [Fact]
    public void ApiExceptionFilter_Should_Return_StatusCodeBadRequest_For_InvalidValueException()
    {
        string exceptionMessage = "Invalid id";

        var loggerMock = new Mock<ILogger<ApiExceptionFilter>>();
        var filter = new ApiExceptionFilter(loggerMock.Object);

        var routeData = new RouteData();
        var httpContext = new DefaultHttpContext();
        var actionDescriptor = new ActionDescriptor();

        var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);

        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new InvalidValueException(exceptionMessage),
        };

        filter.OnException(exceptionContext);

        var result = Assert.IsType<ObjectResult>(exceptionContext.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        var response = Assert.IsType<ResponseErrorsDTO>(result.Value);
        Assert.Contains(exceptionMessage, response.ErrorMessages);
    }

    [Fact]
    public void ApiExceptionFilter_Should_Return_StatusCodeNotFound_For_NotFoundException() 
    {
        string exceptionMessage = "Not Found";

        var loggerMock = new Mock<ILogger<ApiExceptionFilter>>();
        var filter = new ApiExceptionFilter(loggerMock.Object);

        var routeData = new RouteData();
        var httpContext = new DefaultHttpContext();
        var actionDescriptor = new ActionDescriptor();

        var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);

        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new NotFoundException(exceptionMessage),
        };

        filter.OnException(exceptionContext);

        var result = Assert.IsType<ObjectResult>(exceptionContext.Result);
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        var response = Assert.IsType<ResponseErrorsDTO>(result.Value);
        Assert.Contains(exceptionMessage, response.ErrorMessages);
    }
}
