namespace CQ.AuthProvider.BusinessLogic.Invitations;

public sealed record DeclainInvitationArgs(
    string Email,
    int Code);
