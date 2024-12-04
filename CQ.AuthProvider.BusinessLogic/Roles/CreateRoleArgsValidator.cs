using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.BusinessLogic.Roles;

internal sealed class CreateRoleArgsValidator
    : AbstractValidator<CreateRoleArgs>
{
    public CreateRoleArgsValidator()
    {
        RuleFor(r => r.Name)
            .RequiredString();

        RuleFor(r => r.Description)
            .RequiredString();

        RuleFor(r => r.IsDefault)
            .Required();

        RuleFor(r => r.IsPublic)
            .Required();

        RuleFor(r => r.AppId)
            .ValidId();

        RuleFor(r => r.PermissionsKeys)
            .Required()
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

        var accountLoggedHasApp = accountLogged.AppsIds.Contains(args.AppId.Value);
        if (args.AppId == null || accountLoggedHasApp)
        {
            return validationResult;
        }

        validationResult.Errors.Add(new ValidationFailure("AppId", $"Account doen't have this AppId ({args.AppId})"));

        return validationResult;
    }
}
