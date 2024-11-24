using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Accounts;
internal sealed class UpdatePasswordArgsValidator
    : AbstractValidator<UpdatePasswordArgs>
{
    public UpdatePasswordArgsValidator()
    {
        RuleFor(a => a.Email)
            .Email();

        RuleFor(a => a.OldPassword)
            .Password();

        RuleFor(a => a.NewPassword)
            .Password();
    }
}
