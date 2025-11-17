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
        var validationResult = new ValidationResult();
        var accountLogged = (AccountLogged)actionExecutingContext.HttpContext.Items[ContextItem.AccountLogged];

        var args = (CreateBulkPermissionArgs)validationContext.InstanceToValidate;
        var permissions = args.Permissions;

        var appsIds = permissions
                    .GroupBy(a => a.AppId)
                    .Select(g => g.Key!)
                    .ToList();

        if (appsIds.Count != 0)
        {
            var validAppsIds = accountLogged.AppsIds;

            var invalidAppsIds = appsIds
                .Where(id => !validAppsIds.Contains(id))
                .ToList();

            validationResult.Errors.Add(new ValidationFailure("AppId",$"Invalid apps ids {string.Join(",", invalidAppsIds)}"));
        }

        var existPermissionsWithoutAppId = permissions.Exists(p => Guard.IsNull(p.AppId));
        if (existPermissionsWithoutAppId && accountLogged.AppLogged.Id == AuthConstants.AUTH_WEB_API_APP_ID)
        {
            validationResult.Errors.Add(new ValidationFailure("AppId", "Can't create to auth api app"));
        }

        return validationResult;
    }

    public IValidationContext? BeforeValidation(
        ActionExecutingContext actionExecutingContext,
        IValidationContext validationContext) => null;
}
