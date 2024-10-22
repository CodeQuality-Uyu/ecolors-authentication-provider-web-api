using FluentValidation;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CQ.AuthProvider.WebApi.Middlewares;

internal sealed class ValidatorInjectedMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Retrieve the action descriptor from the current context
        var endpoint = context.GetEndpoint();

        if (endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>() is ControllerActionDescriptor actionDescriptor)
        {
            // Check parameters (e.g., action method parameters like CreateLocationRequest)
            foreach (var parameter in actionDescriptor.Parameters)
            {
                var parameterType = parameter.ParameterType;

                // Check if a validator exists for the parameter type
                var validatorType = typeof(IValidator<>).MakeGenericType(parameterType);
                var validator = serviceProvider.GetService(validatorType);

                // Throw an exception if no validator is registered
                if (validator == null)
                {
                    throw new InvalidOperationException($"No validator registered for type {parameterType.FullName}");
                }
            }
        }

        // Continue to the next middleware if validation passes
        await next(context);
    }
}
