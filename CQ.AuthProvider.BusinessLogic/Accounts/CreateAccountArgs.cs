namespace CQ.AuthProvider.BusinessLogic.Accounts;

public sealed record CreateAccountArgs(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Locale,
    string TimeZone,
    string? ProfilePictureId,
    string? RoleId,
    string AppId);
/*
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
