using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Roles;

internal sealed class CreateRoleArgsValidator
    : AbstractValidator<CreateRoleArgs>
{
    public CreateRoleArgsValidator()
    {
        RuleFor(r => r.Name)
            .RequiredString();

        RuleFor(r => r.Description)
            .RequiredString();

        RuleFor(r => r.PermissionKeys)
            .Must(permissionKeys =>
            {
                var duplicatedKeys = permissionKeys
                .GroupBy(g => g)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

                return duplicatedKeys.Count == 0;
            })
            .WithMessage("Duplicated permission keys");

        RuleFor(r => r.AppId)
            .ValidId();
    }
}
