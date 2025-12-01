using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.Utility;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Interceptors;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

internal sealed class CreateBulkPermissionArgsValidator
    : AbstractValidator<CreateBulkPermissionArgs>,
    IValidatorInterceptor
{
    public CreateBulkPermissionArgsValidator()
    {
        RuleForEach(a => a.Permissions)
            .SetValidator(new CreatePermissionArgsValidator());

        RuleFor(a => a.Permissions)
            .Must(permissions =>
            {
                var duplicatedKeys = permissions
                .GroupBy(p => p.Key)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

                return duplicatedKeys.Count == 0;
            })
            .WithMessage("Duplicated permissions keys");
    }

    public ValidationResult? AfterValidation(
        ActionExecutingContext actionExecutingContext,
        IValidationContext validationContext)
    {
        var accountLogged = (AccountLogged)actionExecutingContext.HttpContext.Items[ContextItem.AccountLogged];
        var args = (CreateBulkPermissionArgs)validationContext.InstanceToValidate;

        var permissions = args.Permissions;

        var appsIds = permissions
                    .GroupBy(a => a.AppId)
                    .Select(g => g.Key!)
                    .ToList();

        var invalidAppsIds = appsIds
                        .Where(id => !accountLogged.AppsIds.Contains(id))
                        .ToList();
        if (invalidAppsIds.Count != 0)
        {
            actionExecutingContext.ModelState.AddModelError(
                "AppId",
                $"Account doen't have this AppsIds ({string.Join(",", invalidAppsIds)})");
        }

        var appIsAuth = accountLogged.AppLogged.Id == AuthConstants.AUTH_WEB_API_APP_ID;
        var isWebApiOwner = accountLogged.IsInRole(AuthConstants.AUTH_WEB_API_OWNER_ROLE_ID);
        var permissionToAuthApp = permissions.Exists(p => p.AppId == AuthConstants.AUTH_WEB_API_APP_ID || p.AppId == Guid.Empty);
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
