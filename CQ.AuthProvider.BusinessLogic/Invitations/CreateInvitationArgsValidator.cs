using CQ.AuthProvider.BusinessLogic.Permissions;
using FluentValidation;

namespace CQ.AuthProvider.BusinessLogic.Invitations;

internal sealed class CreateInvitationArgsValidator
    : AbstractValidator<CreateInvitationArgs>
{
    public CreateInvitationArgsValidator()
    {
        RuleFor(i => i.Email)
            .RequiredEmail();

        RuleFor(i => i.RoleId)
            .ValidId();

        RuleFor(i => i.AppId)
            .ValidId();
    }
}
