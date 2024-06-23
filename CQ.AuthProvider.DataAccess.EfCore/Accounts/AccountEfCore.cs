using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountEfCore
{
    public string Id { get; init; } = null!;

    public string Email { get; set; } = null!;

    public string? ProfilePictureUrl { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public List<AccountRole> Roles { get; set; } = [];

    public string Locale { get; set; } = null!;

    public string TimeZone { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public AccountEfCore()
    {
    }

    public AccountEfCore(
        string id,
        string email,
        string fullName,
        string firstName,
        string lastName,
        string locale,
        string timeZone,
        List<Role> roles,
        string? profilePictureUrl)
    {
        Id = id;
        Email = email;
        FullName = fullName;
        FirstName = firstName;
        LastName = lastName;
        ProfilePictureUrl = profilePictureUrl;
        Locale = locale;
        TimeZone = timeZone;
        Roles = roles.ConvertAll(r => new AccountRole(r.Id));
    }
}
