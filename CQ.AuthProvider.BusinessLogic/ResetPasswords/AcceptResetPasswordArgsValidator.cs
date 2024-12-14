using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.Utility;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords;
internal sealed class AcceptResetPasswordArgsValidator
    : AbstractValidator<AcceptResetPasswordArgs>
{
    public AcceptResetPasswordArgsValidator()
    {
        RuleFor(r => r.Email)
            .RequiredEmail();

        RuleFor(r => r.NewPassword)
            .RequiredPassword();

        RuleFor(r => r.Code)
            .Required()
            .InclusiveBetween(100000, 999999);
    }
}
