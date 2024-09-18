namespace CQ.AuthProvider.WebApi.Controllers.Invitations.Models;

public readonly struct InvitationBasicInfoResponse
{
    public string Id { get; init; }

    public string Email { get; init; }

    public bool HasExpired { get; init; }
}
