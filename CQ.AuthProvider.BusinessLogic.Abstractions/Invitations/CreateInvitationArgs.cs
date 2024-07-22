
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;

public readonly struct CreateInvitationArgs
{
    public string Email { get; init; }

    public string RoleId { get; init; }

    public string? AppId { get; init; }

    public CreateInvitationArgs(
        string email,
        string roleId,
        string? appId)
    {
        Email = Guard.Encode(email, nameof(email));
        Guard.ThrowIsInputInvalidEmail(Email);

        Db.ThrowIsInvalidId(roleId, nameof(roleId));
        RoleId = roleId;

        if (Guard.IsNotNullOrEmpty(appId))
        {
            Db.ThrowIsInvalidId(
                appId!,
                nameof(appId));
        }
        AppId = appId;
    }
}
