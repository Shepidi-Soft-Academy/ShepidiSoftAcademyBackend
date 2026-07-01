using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShepidiSoft.Application;

public class FluentValidationFilter : IAsyncActionFilter, IAsyncExceptionFilter
{
    private readonly ILogger<FluentValidationFilter> _logger;

    public FluentValidationFilter(ILogger<FluentValidationFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            _logger.LogWarning("ModelState invalid | TraceId: {TraceId} | Errors: {@Errors}",
                context.HttpContext.TraceIdentifier,
                errors);

            context.Result = new BadRequestObjectResult(ServiceResult.Fail(errors));
            return;
        }

        await next();
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;

        var traceId = context.HttpContext.TraceIdentifier;
        var path = context.HttpContext.Request.Path;
        var method = context.HttpContext.Request.Method;

        _logger.LogError(exception,
            "Unhandled exception | TraceId: {TraceId} | Path: {Path} | Method: {Method}",
            traceId, path, method);

        context.Result = new ObjectResult(ServiceResult.Fail("Bir hata meydana geldi."))
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;

        return Task.CompletedTask;
    }
}