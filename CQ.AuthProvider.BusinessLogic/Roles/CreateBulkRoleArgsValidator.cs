using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Utils;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Interceptors;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Roles;

internal sealed class CreateBulkRoleArgsValidator
    : AbstractValidator<CreateBulkRoleArgs>,
    IValidatorInterceptor
{
    public CreateBulkRoleArgsValidator()
    {
        RuleForEach(a => a.Roles)
            .SetValidator(new CreateBasicRoleArgsValidator());

        RuleFor(a => a.Roles)
            .Must(roles =>
            {
                var duplicatedNames = roles
                .GroupBy(p => p.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

                return duplicatedNames.Count == 0;
            })
            .WithMessage("Duplicated names");

        RuleFor(a => a.AppId)
            .ValidId();
    }

    public ValidationResult? AfterValidation(ActionExecutingContext actionExecutingContext, IValidationContext validationContext)
    {
        var accountLogged = (AccountLogged)actionExecutingContext.HttpContext.Items[ContextItem.AccountLogged];
        var args = (CreateBulkRoleArgs)validationContext.InstanceToValidate;

        var roles = args.Roles;

        var invalidAppsIds = !accountLogged.AppsIds.Contains(args.AppId);
        if (invalidAppsIds)
        {
            actionExecutingContext.ModelState.AddModelError(
                "AppId",
                $"Account doeen't have this AppsIds ({args.AppId})");
        }

        var defaultRoles = args
        .Roles
        .Where(r => r.IsDefault)
        .ToList();

        if (defaultRoles.Count > 1)
        {
            actionExecutingContext.ModelState.AddModelError(
                "IsDefault",
                $"Only one role can be default in an app");
        }

        var appIsAuth = accountLogged.AppLogged.Id == AuthConstants.AUTH_WEB_API_APP_ID;
        var isWebApiOwner = accountLogged.IsInRole(AuthConstants.AUTH_WEB_API_OWNER_ROLE_ID);
        var permissionToAuthApp = args.AppId == AuthConstants.AUTH_WEB_API_APP_ID;
        var authorizedToAuth = appIsAuth && isWebApiOwner;

        if (!authorizedToAuth && permissionToAuthApp)
        {
            actionExecutingContext.ModelState.AddModelError(
                "AppId",
                $"Can't create to auth api app");
        }

        return null;
    }

    public IValidationContext? BeforeValidation(
        ActionExecutingContext actionExecutingContext,
        IValidationContext validationContext) => null;
}

internal sealed class CreateBasicRoleArgsValidator
    : AbstractValidator<CreateBasicRoleArgs>
{
    public CreateBasicRoleArgsValidator()
    {
        RuleFor(r => r.Name)
            .Required();

        RuleFor(r => r.Description)
            .Required();

        RuleFor(r => r.IsPublic)
            .Required();

        RuleFor(r => r.PermissionKeys)
            .Must(permissionKeys =>
            {
                var duplicatedKeys = permissionKeys
                .GroupBy(k => k)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

                return duplicatedKeys.Count == 0;
            })
            .WithMessage("Duplicated permission keys");
    }
}
