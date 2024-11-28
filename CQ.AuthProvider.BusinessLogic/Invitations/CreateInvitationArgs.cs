namespace CQ.AuthProvider.BusinessLogic.Invitations;

public sealed record CreateInvitationArgs(
    string Email,
    Guid RoleId,
    Guid? AppId);
