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
            .Required();

        RuleFor(a => a.Description)
            .Required();

        RuleFor(a => a.Key)
            .Required();

        RuleFor(a => a.AppId)
            .ValidId();
    }

    public ValidationResult? AfterValidation(
        ActionExecutingContext actionExecutingContext,
        IValidationContext validationContext)
    {
        var httpCtx = actionExecutingContext.HttpContext;

        var accountLogged = (AccountLogged)httpCtx.Items[ContextItem.AccountLogged];
        var args = (CreatePermissionArgs)validationContext.InstanceToValidate;

        if (Guard.IsNull(args.AppId))
        {
            return null;
        }

        var hasApp = accountLogged.AppsIds.Contains(args.AppId);

        var authorized = hasApp;
        if (!authorized)
        {
            actionExecutingContext.ModelState.AddModelError(
                nameof(args.AppId),
                $"Account doesn't have this AppId ({args.AppId})");
        }

        var appIsAuth = accountLogged.AppLogged.Id == AuthConstants.AUTH_WEB_API_APP_ID;
        var isWebApiOwner = accountLogged.IsInRole(AuthConstants.AUTH_WEB_API_OWNER_ROLE_ID);
        var permissionToAuthApp = args.AppId == AuthConstants.AUTH_WEB_API_APP_ID || args.AppId == Guid.Empty;

        var authorizedToAuth = appIsAuth && isWebApiOwner;

        if (!authorizedToAuth && permissionToAuthApp)
        {
            actionExecutingContext.ModelState.AddModelError(
                nameof(args.AppId),
                $"Can't create to auth api app");
        }

        return null;
    }

    public IValidationContext? BeforeValidation(
        ActionExecutingContext actionExecutingContext,
        IValidationContext validationContext) => null;
}


public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, TProp> Required<T, TProp>(this IRuleBuilder<T, TProp> validator)
    {
        var options = validator
            .NotNullWithMessage()
            .NotEmpty()
            .WithMessage("Can't be empty"); ;

        return options;
    }

    public static IRuleBuilderOptions<T, TProp> NotNullWithMessage<T, TProp>(this IRuleBuilder<T, TProp> validator)
    {
        var options = validator
            .NotNull()
            .WithMessage("Can't be null");

        return options;
    }

    public static IRuleBuilderOptions<T, Guid?> ValidId<T>(this IRuleBuilder<T, Guid?> validator)
    {
        var options = validator
            .Must((id) =>
            {
                if (Guard.IsNull(id))
                {
                    return true;
                }

                return id != Guid.Empty;
            })
            .WithMessage("Invalid id");

        return options;
    }

    public static IRuleBuilderOptions<T, List<Guid>?> ValidIds<T>(this IRuleBuilder<T, List<Guid>?> validator)
    {
        var options = validator
            .Must((ids) =>
            {
                if (Guard.IsNullOrEmpty(ids))
                {
                    return true;
                }

                var notEmptyIds = !ids.Contains(Guid.Empty);

                return notEmptyIds;
            })
            .WithMessage("Invalid empty id")
            .Must((ids) =>
            {
                if (Guard.IsNullOrEmpty(ids))
                {
                    return true;
                }

                var notDuplicatedIds = !ids
                .GroupBy(r => r)
                .Where(g => g.Count() > 1)
                .Any();

                return notDuplicatedIds;
            })
            .WithMessage("Duplicated ids");

        return options;
    }

    public static IRuleBuilderOptions<T, Guid> ValidId<T>(this IRuleBuilder<T, Guid> validator)
    {
        var options = validator
            .Must((id) => id != Guid.Empty)
            .WithMessage("Invalid id");

        return options;
    }

    public static IRuleBuilderOptions<T, string?> RequiredEmail<T>(this IRuleBuilder<T, string?> validator)
    {
        var options = validator
            .Required()
            .EmailAddress()
            .WithMessage("Invalid format");

        return options;
    }

    public static IRuleBuilderOptions<T, string?> RequiredPassword<T>(this IRuleBuilder<T, string?> validator)
    {
        var options = validator
            .Required()
            .MinimumLength(6)
            .WithMessage("Minimum 6 characters")
            .Must(password =>
            {
                try
                {
                    Guard.ThrowIsInputInvalidPassword(password);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            })
            .WithMessage("Invalid, must have a special character");

        return options;
    }
}