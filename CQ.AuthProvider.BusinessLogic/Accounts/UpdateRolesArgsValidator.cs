using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.Utility;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Accounts;
internal sealed class UpdateRolesArgsValidator
    : AbstractValidator<UpdateRolesArgs>
{
    public UpdateRolesArgsValidator()
    {
        RuleFor(a => a.RoleIds)
            .Required()
            .Must((ids) =>
            {
                if (Guard.IsNull(ids))
                {
                    return false;
                }

                var notEmptyId = !ids.Contains(Guid.Empty);
                return notEmptyId;
            })
            .WithMessage("Invalid empty ids");
    }
}
