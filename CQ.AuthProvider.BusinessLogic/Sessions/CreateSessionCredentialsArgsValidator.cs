
using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Sessions;
internal sealed class CreateSessionCredentialsArgsValidator
    : AbstractValidator<CreateSessionCredentialsArgs>
{
    public CreateSessionCredentialsArgsValidator()
    {
        RuleFor(a => a.Email)
            .RequiredEmail();

        RuleFor(a => a.Password)
            .RequiredPassword();

        RuleFor(a => a.AppId)
            .ValidId();
    }
}
