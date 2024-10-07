using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountEfCore()
{
    public string Id { get; init; } = Db.NewId();

    public string Email { get; init; } = null!;

    public string? ProfilePictureId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Locale { get; set; } = null!;

    public string TimeZone { get; set; } = null!;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow.Date;

    public string? TenantId { get; set; }

    public TenantEfCore? Tenant { get; init; }

    public List<RoleEfCore> Roles { get; init; } = [];

    public List<AppEfCore> Apps { get; init; } = [];

    // For new AccountEfCore
    internal AccountEfCore(Account account)
        : this()
    {
        Id = account.Id;
        Email = account.Email;
        FullName = account.FullName;
        FirstName = account.FirstName;
        LastName = account.LastName;
        Locale = account.Locale;
        TimeZone = account.TimeZone;
        ProfilePictureId = account.ProfilePictureId;
        TenantId = account.Tenant?.Id;
    }
}
