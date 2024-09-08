using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Sessions;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore;

public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options)
    : EfCoreContext(options)
{
    public DbSet<TenantEfCore> Tenants { get; set; }

    public DbSet<AccountEfCore> Accounts { get; set; }

    public DbSet<AccountRole> AccountsRoles { get; set; }

    public DbSet<AccountApp> AccountsApps { get; set; }

    public DbSet<RoleEfCore> Roles { get; set; }

    public DbSet<RolePermission> RolesPermissions { get; set; }

    public DbSet<PermissionEfCore> Permissions { get; set; }

    public DbSet<SessionEfCore> Sessions { get; set; }

    public DbSet<ResetPasswordEfCore> ResetPasswords { get; set; }

    public DbSet<AppEfCore> Apps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        const string seedAccountId = "5a0d9e179991499e80db0a15fda4df79";
        const string authWebApiOwnerRoleId = "4c00f792d8ed43768846711094902d8c";
        const string tenantOwnerRoleId = "5c2260fc58864b75a4cad5c0e7dd57cb";
        const string seedTenantId = "b22fcf202bd84a97936ccf2949e00da4";
        const string authWebApiAppId = "d31184dabbc6435eaec86694650c2679";

        modelBuilder.Entity<TenantEfCore>(entity =>
        {
            entity
            .HasData(
                new TenantEfCore
                {
                    Id = seedTenantId,
                    Name = "Seed Tenant",
                    OwnerId = seedAccountId
                });
        });

        modelBuilder.Entity<AppEfCore>(entity =>
        {
            entity
            .HasData(
                new AppEfCore
                {
                    Id = authWebApiAppId,
                    Name = "Auth Provider WEB API",
                    IsDefault = true,
                    TenantId = seedTenantId,
                });
        });

        modelBuilder.Entity<AccountEfCore>(entity =>
        {
            entity
            .HasMany(a => a.Roles)
            .WithMany()
            .UsingEntity<AccountRole>(
                r => r
                .HasOne(a => a.Role)
                .WithMany()
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.NoAction),
                p => p
                .HasOne(p => p.Account)
                .WithMany()
                .HasForeignKey(a => a.AccountId)
                .OnDelete(DeleteBehavior.NoAction))
            .HasData(
                new AccountRole
                {
                    Id = "9dc669b28b0f4f3fb8a832961a76a6c9",
                    AccountId = seedAccountId,
                    RoleId = authWebApiOwnerRoleId,
                })
            ;

            entity
            .HasMany(a => a.Apps)
            .WithMany()
            .UsingEntity<AccountApp>(
                r => r
                .HasOne(a => a.App)
                .WithMany()
                .HasForeignKey(a => a.AppId)
                .OnDelete(DeleteBehavior.NoAction),
                p => p
                .HasOne(p => p.Account)
                .WithMany()
                .HasForeignKey(a => a.AccountId)
                .OnDelete(DeleteBehavior.NoAction))
            .HasData(
                new AccountApp
                {
                    Id = "ef03980ea2a54e4bba92e022fbd33d9b",
                    AccountId = seedAccountId,
                    AppId = authWebApiAppId,
                })
            ;
            
            entity
            .HasOne(a => a.Tenant)
            .WithMany()
            .HasForeignKey(a => a.TenantId)
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasData(new AccountEfCore
            {
                Id = seedAccountId,
                FirstName = "Seed",
                LastName = "Seed",
                FullName = "Seed Seed",
                Email = "seed@cq.com",
                Locale = "Uruguay",
                TimeZone = "-3",
                ProfilePictureId = null,
                TenantId = string.Empty,
                CreatedAt = new DateTime(2024, 1, 1),
            });
        });

        const string jokerPermissionId = "e2d42874c56e46319b21eeb817f3b988";
        const string fullAccessPermissionId = "920d910719224496b575618a9495d2c4";
        modelBuilder.Entity<PermissionEfCore>(entity =>
        {
            entity
            .HasOne(p => p.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            entity.HasData(
                new PermissionEfCore
                {
                    Id = "d40ad347c7f943e399069eef409b4fa6",
                    Name = "Can read permissions",
                    Description = "Can read permissions",
                    Key = "getall-permission",
                    AppId = authWebApiAppId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = "aca002cfbf3a47899ff4c16e6be2c029",
                    Name = "Can read roles",
                    Description = "Can read roles",
                    Key = "getall-role",
                    AppId = authWebApiAppId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = "d56a38db0db2439f8ee15a142b22b33b",
                    Name = "Can read permissions of tenant",
                    Description = "Can read permissions of tenant",
                    Key = PermissionKey.CanReadPermissionsOfTenant,
                    AppId = authWebApiAppId,
                    IsPublic = false,
                },
                new PermissionEfCore
                {
                    Id = "e0132221c91f44ada257a38d951407d6",
                    Name = "Can read private permissions",
                    Description = "Can read private permissions",
                    Key = PermissionKey.CanReadPrivatePermissions,
                    AppId = authWebApiAppId,
                    IsPublic = false,
                },
                new PermissionEfCore
                {
                    Id = "05276f2a25dc4db5b37b0948e05c35ab",
                    Name = "Can read roles of tenant",
                    Description = "Can read roles of tenant",
                    Key = PermissionKey.CanReadRolesOfTenant,
                    AppId = authWebApiAppId,
                    IsPublic = false,
                },
                new PermissionEfCore
                {
                    Id = "1ce9908dba38490cbc65389bfeece21e",
                    Name = "Can read private roles",
                    Description = "Can read private roles",
                    Key = PermissionKey.CanReadPrivateRoles,
                    AppId = authWebApiAppId,
                    IsPublic = false,
                },
                new PermissionEfCore
                {
                    Id = "80ca0e41ea5046519f351a99b03b294e",
                    Name = "Can read invitations of tenant",
                    Description = "Can read invitations of tenant",
                    Key = PermissionKey.CanReadInvitationsOfTenant,
                    AppId = authWebApiAppId,
                    IsPublic = false,
                    TenantId = seedTenantId
                },
                new PermissionEfCore
                {
                    Id = jokerPermissionId,
                    Name = "Joker",
                    Description = "Joker",
                    Key = PermissionKey.Joker,
                    AppId = authWebApiAppId,
                    IsPublic = false,
                },
                new PermissionEfCore
                {
                    Id = fullAccessPermissionId,
                    Name = "Full access",
                    Description = "Full accesss",
                    Key = PermissionKey.FullAccess,
                    AppId = authWebApiAppId,
                    IsPublic = false,
                    TenantId = seedTenantId,
                });
        });

        modelBuilder.Entity<RoleEfCore>(entity =>
        {
            entity
            .HasOne(r => r.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<RolePermission>(
                r => r
                .HasOne(p => p.Permission)
                .WithMany()
                .HasForeignKey(a => a.PermissionId)
                .OnDelete(DeleteBehavior.NoAction),
                r => r
                .HasOne(p => p.Role)
                .WithMany()
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.NoAction))
            .HasData(
                new RolePermission
                {
                    Id = "080873f63bff4e9a9687ac70658b710b",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = jokerPermissionId,
                },
                new RolePermission
                {
                    Id = "4909564462b040289d0dc0758cf8942e",
                    RoleId = authWebApiOwnerRoleId,
                    PermissionId = jokerPermissionId,
                },
                new RolePermission
                {
                    Id = "64ec1b6bbd3d4c49b609c0f58359e7ac",
                    RoleId = authWebApiOwnerRoleId,
                    PermissionId = jokerPermissionId,
                });

            entity
            .HasData(
                new RoleEfCore
                {
                    Id = authWebApiOwnerRoleId,
                    Name = "Auth WEB API Owner",
                    Description = "Auth WEB API Owner",
                    AppId = authWebApiAppId,
                    IsPublic = false,
                    TenantId = seedTenantId,
                    IsDefault = false,
                },
                new RoleEfCore
                {
                    Id = tenantOwnerRoleId,
                    Name = "Tenant Owner",
                    Description = "Tenant Owner",
                    AppId = authWebApiAppId,
                    IsPublic = true,
                    TenantId = seedTenantId,
                    IsDefault = false,
                });
        });

        modelBuilder.Entity<SessionEfCore>(entity =>
        {
            entity
            .HasOne(s => s.App)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
