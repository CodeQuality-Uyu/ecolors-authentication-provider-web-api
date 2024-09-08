using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts.Models;

public sealed record class CreateAccountRequest : Request<CreateAccountArgs>
{
    public string? Email { get; init; }

    public string? Password { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? ProfilePictureUrl { get; init; }

    public string? Role { get; init; }

    public string? Locale { get; init; }

    public string? TimeZone { get; init; }

    public string? AppId { get; init; }

    protected override CreateAccountArgs InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(Email, nameof(Email));
        Guard.ThrowIsNullOrEmpty(Password, nameof(Password));
        Guard.ThrowIsNullOrEmpty(FirstName, nameof(FirstName));
        Guard.ThrowIsNullOrEmpty(LastName, nameof(LastName));
        Guard.ThrowIsNullOrEmpty(ProfilePictureUrl, nameof(ProfilePictureUrl));
        Guard.ThrowIsNullOrEmpty(Role, nameof(Role));
        Guard.ThrowIsNullOrEmpty(Locale, nameof(Locale));
        Guard.ThrowIsNullOrEmpty(TimeZone, nameof(TimeZone));
        Guard.ThrowIsNullOrEmpty(AppId, nameof(AppId));

        return new CreateAccountArgs(
           Email!,
           Password!,
           FirstName!,
            LastName!,
            Locale!,
            TimeZone!,
            Role,
            ProfilePictureUrl,
            AppId!);
    }
}
