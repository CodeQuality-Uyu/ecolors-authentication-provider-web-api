using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Tenants;
internal sealed class CreateTenantArgsValidator
    : AbstractValidator<CreateTenantArgs>
{
    public CreateTenantArgsValidator()
    {
        RuleFor(c => c.Name)
            .Required();

        RuleFor(c => c.MiniLogoId)
            .ValidId();

        RuleFor(c => c.CoverLogoId)
            .ValidId();
    }
}
