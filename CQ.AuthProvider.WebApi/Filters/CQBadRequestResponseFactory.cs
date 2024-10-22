using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace CQ.AuthProvider.WebApi.Filters;

public class CQBadRequestResponseFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        return new BadRequestObjectResult(new
        {
            InnerCode = "InvalidRequest",
            Title = "Validation errors",
            Description = "Some of the provided values are incorrect",
            Errors = validationProblemDetails?.Errors
        });
    }
}
