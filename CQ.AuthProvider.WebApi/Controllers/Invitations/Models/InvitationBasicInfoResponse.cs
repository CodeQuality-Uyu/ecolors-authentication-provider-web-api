namespace CQ.AuthProvider.WebApi.Controllers.Invitations.Models;

public sealed record InvitationBasicInfoResponse(
    Guid Id,
    string Email)
{
    public bool HasExpired { get; init; }
}
