
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

namespace CQ.AuthProvider.DataAccess.Factory;

public sealed record class AuthSection
{
    public const string Name = "Auth";

    public AuthEngine Engine { get; init; }

    public FakeAccountLogged? Logged { get; init; }
}

public enum AuthEngine
{
    Sql,
    Mongo
}

public sealed record class FakeAccountLogged : AccountLogged
{
    public bool IsActive { get; init; }

    public List<string> AppsIds { get; init; } = [];

    public List<string> PermissionsKeys { get; init; } = [];

    public string TenantId { get; init; } = null!;

    public AccountLogged Account { get; private set; } = null!;

    public void Build()
    {
        Account = new()
        {
            Id = Id,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            FullName = FullName,
            ProfilePictureUrl = ProfilePictureUrl,
            Locale = Locale,
            TimeZone = TimeZone,
            Token = Token,
            Apps = AppsIds.ConvertAll(a => new App()
            {
                Id = a
            }),
            Roles = [
            new Role
            {
                Permissions = PermissionsKeys.ConvertAll(p => new Permission
                {
                    Id = p
                })
            }],
            Tenant = new Tenant
            {
                Id = TenantId
            }
        };
    }
}
