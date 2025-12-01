using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
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
            .SetValidator(new CreateRoleArgsValidator());

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
    }

    public ValidationResult? AfterValidation(ActionExecutingContext actionExecutingContext, IValidationContext validationContext)
    {
        var accountLogged = (AccountLogged)actionExecutingContext.HttpContext.Items[ContextItem.AccountLogged];
        var args = (CreateBulkRoleArgs)validationContext.InstanceToValidate;

        var roles = args.Roles;

        var appsIds = roles
            .GroupBy(a => a.AppId)
            .Select(g => g.Key)
            .ToList();

        var invalidAppsIds = appsIds
            .Where(a => !accountLogged.AppsIds.Contains(a))
            .ToList();
        if (invalidAppsIds.Count != 0)
        {
            actionExecutingContext.ModelState.AddModelError(
                "AppId",
                $"Account doen't have this AppsIds ({string.Join(",", invalidAppsIds)})");
        }

        var defaultRoles = args
        .Roles
        .Where(r => r.IsDefault)
        .GroupBy(r => r.AppId)
        .ToList();

        var duplicatedDefaultRolesInApp = defaultRoles
            .Where(g => g.Count() > 1)
            .ToList();
        if (duplicatedDefaultRolesInApp.Count > 1)
        {
            actionExecutingContext.ModelState.AddModelError(
                "IsDefault",
                $"Only one role can be default in an app");
        }

        var appIsAuth = accountLogged.AppLogged.Id == AuthConstants.AUTH_WEB_API_APP_ID;
        var isWebApiOwner = accountLogged.IsInRole(AuthConstants.AUTH_WEB_API_OWNER_ROLE_ID);
        var permissionToAuthApp = roles.Exists(r => r.AppId == AuthConstants.AUTH_WEB_API_APP_ID || r.AppId == Guid.Empty);
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
