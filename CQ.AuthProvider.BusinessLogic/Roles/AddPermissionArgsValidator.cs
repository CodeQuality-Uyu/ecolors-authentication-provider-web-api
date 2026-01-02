using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Roles;

internal sealed class AddPermissionArgsValidator
    : AbstractValidator<AddPermissionArgs>
{
    public AddPermissionArgsValidator()
    {
        RuleFor(x => x.PermissionIds)
            .Required()
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("PermissionIds contains duplicate values.");
    }
}