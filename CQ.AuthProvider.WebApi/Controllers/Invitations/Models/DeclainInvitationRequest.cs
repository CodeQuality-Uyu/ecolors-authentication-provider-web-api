using CQ.ApiElements.Dtos;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Invitations.Models;

public sealed record class DeclainInvitationRequest
    : Request<string>
{
    public string? Email { get; init; }

    protected override string InnerMap()
    {
        var email = Guard.Encode(Email, nameof(Email));
        Guard.ThrowIsInputInvalidEmail(email);

        return email;
    }
}
