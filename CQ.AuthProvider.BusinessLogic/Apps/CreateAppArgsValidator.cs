using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Apps;

internal sealed class CreateAppArgsValidator
    : AbstractValidator<CreateAppArgs>
{
    public CreateAppArgsValidator()
    {
        RuleFor(a => a.Name)
            .RequiredString();

        RuleFor(a => a.CoverId)
            .ValidId();
    }
}
