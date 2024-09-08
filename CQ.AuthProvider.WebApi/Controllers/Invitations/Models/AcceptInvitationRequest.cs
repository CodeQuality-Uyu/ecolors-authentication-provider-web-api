using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Invitations.Models;

public sealed record class AcceptInvitationRequest
    : Request<AcceptInvitationArgs>
{
    public string? Code { get; init; }

    public string? Email { get; init; }

    public string? Password { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? Locale { get; init; }

    public string? TimeZone { get; init; }

    public string? ProfilePictureId { get; init; }

    protected override AcceptInvitationArgs InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(Code, nameof(Code));
        Guard.ThrowIsNullOrEmpty(Email, nameof(Email));
        Guard.ThrowIsNullOrEmpty(Password, nameof(Password));
        Guard.ThrowIsNullOrEmpty(FirstName, nameof(FirstName));
        Guard.ThrowIsNullOrEmpty(LastName, nameof(LastName));
        Guard.ThrowIsNullOrEmpty(Locale, nameof(Locale));
        Guard.ThrowIsNullOrEmpty(TimeZone, nameof(TimeZone));

        return new AcceptInvitationArgs(
            Email!,
            Password!,
            FirstName!,
            LastName!,
            Locale!,
            TimeZone!,
            ProfilePictureId,
            Code!);
    }
}
