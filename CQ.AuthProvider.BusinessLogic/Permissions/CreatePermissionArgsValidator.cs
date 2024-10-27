using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.Utility;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Interceptors;

namespace CQ.AuthProvider.BusinessLogic.Permissions;
internal sealed class CreatePermissionArgsValidator
    : AbstractValidator<CreatePermissionArgs>,
    IValidatorInterceptor
{
    public CreatePermissionArgsValidator()
    {
        RuleFor(a => a.Name)
            .RequiredString();

        RuleFor(a => a.Description)
            .RequiredString();

        RuleFor(a => a.Key)
            .RequiredString();

        RuleFor(a => a.AppId)
            .ValidId();
    }

    public ValidationResult? AfterValidation(
        ActionExecutingContext actionExecutingContext,
        IValidationContext validationContext)
    {
        var validationResult = new ValidationResult();
        var accountLogged = (AccountLogged)actionExecutingContext.HttpContext.Items[ContextItems.AccountLogged];

        var args = (CreatePermissionArgs)validationContext.InstanceToValidate;

        if (Guard.IsNull(args.AppId) && accountLogged.AppLogged.Id == AuthConstants.AUTH_WEB_API_APP_ID)
        {
            validationResult.Errors.Add(new ValidationFailure("AppId", "Can't create to auth api app"));
        }

        return validationResult;
    }

    public IValidationContext? BeforeValidation(ActionExecutingContext actionExecutingContext, IValidationContext validationContext)
    {
        return null;
    }
}

internal static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> RequiredString<T>(this IRuleBuilder<T, string> validator)
    {
        var options = validator
        .NotNull().WithMessage("Can't be null")
        .NotEmpty().WithMessage("Can't be empty");

        return options;
    }
    public static IRuleBuilderOptions<T, string?> ValidId<T>(this IRuleBuilder<T, string?> validator)
    {
        var options = validator
            .Must((id) =>
            {
                if (Guard.IsNull(id))
                {
                    return true;
                }

                return Db.IsIdValid(id);
            })
            .WithMessage("Invalid id");

        return options;
    }
}