using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;

internal sealed class CreateResetPasswordArgsValidator
    : AbstractValidator<CreateResetPasswordArgs>
{
    public CreateResetPasswordArgsValidator()
    {
        RuleFor(r => r.Email)
            .RequiredEmail();
    }
}
