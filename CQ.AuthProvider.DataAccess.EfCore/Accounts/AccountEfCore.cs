using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountEfCore()
{
    public string Id { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string? ProfilePictureUrl { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Locale { get; set; } = null!;

    public string TimeZone { get; set; } = null!;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow.Date;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    public List<AccountRole> Roles { get; init; } = [];

    public List<AccountApp> Apps { get; init; } = [];

    /// <summary>
    /// For new AccountEfCore
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    /// <param name="fullName"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="locale"></param>
    /// <param name="timeZone"></param>
    /// <param name="roles"></param>
    /// <param name="profilePictureUrl"></param>
    public AccountEfCore(
        string id,
        string email,
        string fullName,
        string firstName,
        string lastName,
        string locale,
        string timeZone,
        List<Role> roles,
        string? profilePictureUrl,
        List<App> apps,
        Tenant tenant)
        : this()
    {
        Id = id;
        Email = email;
        FullName = fullName;
        FirstName = firstName;
        LastName = lastName;
        ProfilePictureUrl = profilePictureUrl;
        Locale = locale;
        TimeZone = timeZone;
        TenantId = tenant.Id;
        Roles = roles.ConvertAll(r => new AccountRole(r.Id, r.Tenant.Id));
        Apps = apps.ConvertAll(a => new AccountApp(a.Id, a.Tenant.Id));
    }

    /// <summary>
    /// For seed data
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    /// <param name="fullName"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="locale"></param>
    /// <param name="timeZone"></param>
    /// <param name="profilePictureUrl"></param>
    internal AccountEfCore(
        string id,
        string email,
        string fullName,
        string firstName,
        string lastName,
        string locale,
        string timeZone,
        string? profilePictureUrl)
        : this()
    {
        Id = id;
        Email = email;
        FullName = fullName;
        FirstName = firstName;
        LastName = lastName;
        ProfilePictureUrl = profilePictureUrl;
        Locale = locale;
        TimeZone = timeZone;
    }
}
