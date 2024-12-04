using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Accounts;

internal sealed class CreateAccountArgsValidator
    : AbstractValidator<CreateAccountArgs>
{
    public CreateAccountArgsValidator()
    {
        RuleFor(a => a.FirstName)
            .RequiredString();

        RuleFor(a => a.LastName)
            .RequiredString();

        RuleFor(a => a.Locale)
            .RequiredString();

        RuleFor(a => a.TimeZone)
            .RequiredString();

        RuleFor(a => a.Email)
            .Email();

        RuleFor(a => a.Password)
            .Password();

        RuleFor(a => a.AppId)
            .ValidId();
    }
}

internal sealed class CreateAccountForArgsValidator
    : AbstractValidator<CreateAccountForArgs>
{
    public CreateAccountForArgsValidator()
    {
        RuleFor(a => a.FirstName)
            .RequiredString();

        RuleFor(a => a.LastName)
            .RequiredString();

        RuleFor(a => a.Locale)
            .RequiredString();

        RuleFor(a => a.TimeZone)
            .RequiredString();

        RuleFor(a => a.Email)
            .Email();

        RuleFor(a => a.Password)
            .Password();

        RuleFor(a => a.RoleId)
            .ValidId();

        RuleFor(a => a.AppId)
            .ValidId();
    }
}
