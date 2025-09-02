
using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Apps;

internal sealed class UpdateDefaultCoinArgsValidator
    : AbstractValidator<UpdateDefaultCoinArgs>
{
    public UpdateDefaultCoinArgsValidator()
    {
        RuleFor(a => a.DefaultCoin)
            .Required()
            .IsInEnum()
            .WithMessage("DefaultCoin value not supported");
    }
}
