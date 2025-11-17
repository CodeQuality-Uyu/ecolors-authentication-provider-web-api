using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Utils;
using FluentValidation;
using FluentValidation.Results;
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

        RuleFor(r => r.PermissionKeys)
            .NotNullWithMessage()
            .NotEmpty()
            .WithMessage("At least one permission key is required")
            .Must(keys =>
            {
                return keys.Count == keys.Distinct(StringComparer.OrdinalIgnoreCase).Count();
            })
            .WithMessage("Can't have duplicated permission keys");
    }

    public ValidationResult? AfterValidation(ActionExecutingContext actionExecutingContext, IValidationContext validationContext)
    {
        var httpCtx = actionExecutingContext.HttpContext;

        var accountLogged = (AccountLogged)httpCtx.Items[ContextItem.AccountLogged];
        var args = (CreateRoleArgs)validationContext.InstanceToValidate;

        // Let your normal FV rules handle nulls, don't override them
        if (args?.AppId is null)
            return null;

        var hasApp = accountLogged.AppsIds.Contains(args.AppId);
        var isWebApiOwner = accountLogged.IsInRole(AuthConstants.AUTH_WEB_API_OWNER_ROLE_ID);
        var isTenantOwner = accountLogged.IsInRole(AuthConstants.TENANT_OWNER_ROLE_ID);

        var authorized = hasApp || isWebApiOwner || isTenantOwner;

        if (!authorized)
        {
            // Add an additional error WITHOUT replacing FV’s result
            actionExecutingContext.ModelState.AddModelError(
                nameof(args.AppId),
                $"Account doesn't have this AppId ({args.AppId})");
        }

        // Return null => keep FluentValidation's original result (plus ModelState error above)
        return null;
    }

    public IValidationContext? BeforeValidation(
        ActionExecutingContext actionExecutingContext,
        IValidationContext validationContext) => validationContext;
}
