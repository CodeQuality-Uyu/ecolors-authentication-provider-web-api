using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Apps;

internal sealed class CreateAppArgsValidator
    : AbstractValidator<CreateAppArgs>
{
    public CreateAppArgsValidator()
    {
        RuleFor(a => a.Name)
            .Required();

        RuleFor(a => a.BackgroundCoverId)
            .ValidId();
    }
}

internal sealed class CreateClientAppArgsValidator
    : AbstractValidator<CreateClientAppArgs>
{
    public CreateClientAppArgsValidator()
    {
        RuleFor(a => a.Name)
            .Required();

        RuleFor(a => a.CoverId)
            .ValidId();

        RuleFor(a => a.BackgroundCoverId)
            .ValidId();
    }
}