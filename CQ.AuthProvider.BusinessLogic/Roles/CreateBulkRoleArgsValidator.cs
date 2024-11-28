using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Utility;
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
        var validationResult = new ValidationResult();
        var accountLogged = (AccountLogged)actionExecutingContext.HttpContext.Items[ContextItem.AccountLogged];

        var args = (CreateBulkRoleArgs)validationContext.InstanceToValidate;
        var roles = args.Roles;

        var appsIds = roles
            .GroupBy(a => a.AppId)
            .Where(g => g.Key.HasValue)
            .Select(g => g.Key)
            .ToList();
        var invalidAppsIds = appsIds
            .Where(a => !accountLogged.AppsIds.Contains(a.Value))
            .ToList();
        if (invalidAppsIds.Count != 0)
        {
            validationResult.Errors.Add(new ValidationFailure("AppId", $"Invalid apps ids {string.Join(",", invalidAppsIds)}"));
        }

        var defaultRoles = args
        .Roles
        .Where(r => r.IsDefault)
        .GroupBy(r => r.AppId ?? accountLogged.AppLogged.Id)
        .ToList();

        var duplicatedDefaultRolesInApp = defaultRoles
            .Where(g => g.Count() > 1)
            .ToList();
        if (duplicatedDefaultRolesInApp.Count > 1)
        {
            validationResult.Errors.Add(new ValidationFailure("AppId", "Only one role can be default in an app"));
        }

        return validationResult;
    }

    public IValidationContext? BeforeValidation(ActionExecutingContext actionExecutingContext, IValidationContext validationContext)
    {
        return null;
    }
}
