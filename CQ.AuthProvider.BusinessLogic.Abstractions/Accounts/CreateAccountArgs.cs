using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

public readonly struct CreateAccountArgs
{
    public string Email { get; init; }

    public string Password { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Locale { get; init; }

    public string TimeZone { get; init; }

    public string? ProfilePictureId { get; init; }

    public string? RoleId { get; init; }

    public string AppId { get; init; }

    public CreateAccountArgs(
        string email,
        string password,
        string firstName,
        string lastName,
        string locale,
        string timeZone,
        string? roleId,
        string? profilePictureId,
        string appId)
    {
        Email = Guard.Encode(email, nameof(email));
        Guard.ThrowIsInputInvalidEmail(Email);

        Password = Guard.Encode(password, nameof(password));
        Guard.ThrowIsInputInvalidPassword(Password);

        FirstName = Guard.Encode(firstName, nameof(firstName));
        LastName = Guard.Encode(lastName, nameof(lastName));

        Locale = Guard.Encode(locale, nameof(locale));
        TimeZone = Guard.Encode(timeZone, nameof(timeZone));

        if (Guard.IsNotNullOrEmpty(roleId))
        {
            Db.ThrowIsInvalidId(roleId, nameof(roleId));
        }
        RoleId = roleId;

        if (Guard.IsNotNullOrEmpty(profilePictureId))
        {
            Db.ThrowIsInvalidId(profilePictureId, nameof(profilePictureId));
        }
        ProfilePictureId = profilePictureId;

        Db.ThrowIsInvalidId(appId, nameof(appId));
        AppId = appId;

        if (Guard.IsNotNullOrEmpty(roleId))
        {
            Db.ThrowIsInvalidId(roleId, nameof(roleId));
        }
        RoleId = roleId;
    }
}
