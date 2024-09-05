using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountEfCore()
{
    public string Id { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string? ProfilePictureId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Locale { get; set; } = null!;

    public string TimeZone { get; set; } = null!;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow.Date;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    public List<RoleEfCore> Roles { get; init; } = [];

    public List<AppEfCore> Apps { get; init; } = [];

    // For new AccountEfCore
    public AccountEfCore(
        string id,
        string email,
        string fullName,
        string firstName,
        string lastName,
        string locale,
        string timeZone,
        List<Role> roles,
        string? profilePictureId,
        List<App> apps,
        Tenant tenant)
        : this()
    {
        Id = id;
        Email = email;
        FullName = fullName;
        FirstName = firstName;
        LastName = lastName;
        ProfilePictureId = profilePictureId;
        Locale = locale;
        TimeZone = timeZone;
        TenantId = tenant.Id;
    }
}
