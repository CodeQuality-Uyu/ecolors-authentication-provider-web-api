using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed class CreateAppArgsValidator
    : AbstractValidator<CreateAppArgs>
{
    public CreateAppArgsValidator()
    {
        RuleFor(a => a.Name)
            .RequiredString();
    }
}
