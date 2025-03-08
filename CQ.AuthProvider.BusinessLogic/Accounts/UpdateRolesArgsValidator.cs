using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Accounts;
internal sealed class UpdateRolesArgsValidator
    : AbstractValidator<UpdateRolesArgs>
{
    public UpdateRolesArgsValidator()
    {
        RuleFor(a => a.RoleIds)
            .ValidIds();
    }
}
