namespace CQ.AuthProvider.BusinessLogic.Invitations;

public sealed record CreateInvitationArgs(
    string Email,
    string RoleId,
    string? AppId);
