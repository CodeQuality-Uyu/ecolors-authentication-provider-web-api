using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Utils;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Interceptors;

namespace CQ.AuthProvider.BusinessLogic.Roles;

internal sealed class CreateRoleArgsValidator
    : AbstractValidator<CreateRoleArgs>,
    IValidatorInterceptor
{
    public CreateRoleArgsValidator()
    {
        RuleFor(r => r.Name)
            .Required();

        RuleFor(r => r.Description)
            .Required();

        RuleFor(r => r.IsPublic)
            .Required();

        RuleFor(r => r.AppId)
            .ValidId();

        RuleFor(r => r.PermissionsKeys)
            .NotNullWithMessage()
            .Must(permissionKeys =>
            {
                if(permissionKeys == null)
                {
                    return false;
                }

                var duplicatedKeys = permissionKeys
                .GroupBy(g => g)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

                return duplicatedKeys.Count == 0;
            })
            .WithMessage("Can't have duplicated permissions keys");
    }

    public ValidationResult? AfterValidation(ActionExecutingContext actionExecutingContext, IValidationContext validationContext)
    {
        var validationResult = new ValidationResult();
        var accountLogged = (AccountLogged)actionExecutingContext.HttpContext.Items[ContextItem.AccountLogged];

        var args = (CreateRoleArgs)validationContext.InstanceToValidate;

        if(args.AppId == null)
        {
            return validationResult;
        }

        var accountLoggedHasApp = accountLogged.AppsIds.Contains(args.AppId);
        if (args.AppId == null || 
            accountLoggedHasApp ||
            accountLogged.IsInRole(AuthConstants.AUTH_WEB_API_OWNER_ROLE_ID.ToString()) ||
            accountLogged.IsInRole(AuthConstants.TENANT_OWNER_ROLE_ID.ToString()))
        {
            return validationResult;
        }

        validationResult.Errors.Add(new ValidationFailure("AppId", $"Account doesn't have this AppId ({args.AppId})"));

        return validationResult;
    }

    public IValidationContext? BeforeValidation(
        ActionExecutingContext actionExecutingContext,
        IValidationContext validationContext) => null;
}
