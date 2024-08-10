using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Rocketseat.Domain.Communications.Responses;
using Rocketseat.Domain.Shared;
using Rocketseat.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Rocketseat.API.Filters;

[ExcludeFromCodeCoverage]
public class ExceptionFilter : IExceptionFilter
{
    private readonly IStringLocalizer<ErrorMessages> _localizer;

    public ExceptionFilter(IStringLocalizer<ErrorMessages> localizer)
    {
        _localizer = localizer;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is RocketseatException)
            HandleProjectException(context);
        else
            ThrowUnknownError(context);
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ErrorOnValidationException:
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(new ErrorResponse(context.Exception.Message));
                break;

            case InvalidException:
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(new ErrorResponse(context.Exception.Message));
                break;

            case NotFoundException:
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Result = new NotFoundObjectResult(new ErrorResponse(context.Exception.Message));
                break;
        }
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ErrorResponse(_localizer["UnknownError"]));
    }
}