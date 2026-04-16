using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShepidiSoft.Application;

public class FluentValidationFilter : IAsyncActionFilter, IAsyncExceptionFilter
{
   
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            var resultModel = ServiceResult.Fail(errors);
            context.Result = new BadRequestObjectResult(resultModel);
            return;
        }

        await next();
    }


    public Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .Select(x => x.ErrorMessage)
                .ToList();

            var resultModel = ServiceResult.Fail(errors);
            context.Result = new BadRequestObjectResult(resultModel);
            context.ExceptionHandled = true;
        }

        return Task.CompletedTask;
    }
}