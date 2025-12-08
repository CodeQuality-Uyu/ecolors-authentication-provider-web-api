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
    }
}

internal sealed class CreateClientAppArgsValidator
    : AbstractValidator<CreateClientAppArgs>
{
    public CreateClientAppArgsValidator()
    {
        RuleFor(a => a.Name)
            .Required();
    }
}

internal sealed class LogoValidator
    : AbstractValidator<Logo>
{
    public LogoValidator()
    {
        RuleFor(a => a.ColorKey)
            .Required();

        RuleFor(a => a.LightKey)
            .Required();

        RuleFor(a => a.DarkKey)
            .Required();
    }
}