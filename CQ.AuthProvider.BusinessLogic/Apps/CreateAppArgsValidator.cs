using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed class CreateAppArgsValidator
    : AbstractValidator<CreateAppArgs>
{
    public CreateAppArgsValidator()
    {
        RuleFor(a => a.Name)
            .NotNull().WithMessage("Can't be null")
            .NotEmpty().WithMessage("Can't be empty");
    }
}
