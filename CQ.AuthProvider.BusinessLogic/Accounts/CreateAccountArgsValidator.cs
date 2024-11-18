using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.Utility;
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
            .RequiredString()
            .EmailAddress()
            .WithMessage("Invalid format");

        RuleFor(a => a.Password)
            .RequiredString()
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

        RuleFor(a => a.RoleId)
            .ValidId();

        RuleFor(a => a.AppId)
            .ValidId();
    }
}
