﻿using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Invitations;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Sessions;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

    public DbSet<InvitationEfCore> Invitations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seedTenantId = Guid.Parse("882a262c-e1a7-411d-a26e-40c61f3b810c");
        var appOwnerRoleId = Guid.Parse("4579a206-b6c7-4d58-9d36-c3e0923041b5");
        var clientOwnerRoleId = Guid.Parse("01e55142-6b8c-4e7e-9d71-1e459d07796d");

        modelBuilder.Entity<TenantEfCore>(entity =>
        {
            entity
            .Ignore(t => t.Owner);

            entity
            .HasMany(t => t.Accounts)
            .WithOne(a => a.Tenant)
            .OnDelete(DeleteBehavior.Restrict);

            entity
            .HasData(
                new TenantEfCore
                {
                    Id = seedTenantId,
                    Name = "Seed Tenant",
                    OwnerId = AuthConstants.SEED_ACCOUNT_ID,
                    MiniLogoId = Guid.Parse("0f491b27-2a93-479a-b674-5c49db77f05c"),
                    CoverLogoId = Guid.Parse("d7cb8b70-f3e9-4ffa-a963-c72942a7f65b"),
                });
        });

        modelBuilder.Entity<AppEfCore>(entity =>
        {
            entity
            .HasMany(a => a.Permissions)
            .WithOne(p => p.App)
            .OnDelete(DeleteBehavior.Cascade);

            entity
            .HasData(
                new AppEfCore
                {
                    Id = AuthConstants.AUTH_WEB_API_APP_ID,
                    Name = "Auth Provider Web Api",
                    IsDefault = true,
                    TenantId = seedTenantId,
                    DefaultCoin = Coin.UYU,
                    BackgroundColor = new CoverBackgroundColorEfCore
                    {
                        Colors = [ "#FFFFFF" ],
                        Config = "linear-gradient(310g, {{colors}})"
                    },
                });

            entity
            .Property(e => e.DefaultCoin)
            .HasDefaultValue(Coin.UYU);

            entity
            .Property(a => a.BackgroundColor)
            .HasConversion(
                v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                v => JsonSerializer.Deserialize<CoverBackgroundColorEfCore>(v, JsonSerializerOptions.Default));
        });

        modelBuilder.Entity<AccountEfCore>(entity =>
        {
            entity
            .HasMany(a => a.Roles)
            .WithMany()
            .UsingEntity<AccountRole>();

            entity
            .HasMany(a => a.Apps)
            .WithMany()
            .UsingEntity<AccountApp>();

            entity
            .HasData(new AccountEfCore
            {
                Id = AuthConstants.SEED_ACCOUNT_ID,
                FirstName = "Seed",
                LastName = "Seed",
                FullName = "Seed Seed",
                Email = "seed@cq.com",
                Locale = "Uruguay",
                TimeZone = "-3",
                ProfilePictureId = null,
                TenantId = seedTenantId,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            });
        });

        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.AccountId });

            entity
            .HasOne(a => a.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(p => p.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            entity
            .HasData(
                new AccountRole
                {
                    AccountId = AuthConstants.SEED_ACCOUNT_ID,
                    RoleId = AuthConstants.SEED_ROLE_ID,
                });
        });

        modelBuilder.Entity<AccountApp>(entity =>
        {
            entity.HasKey(e => new { e.AppId, e.AccountId });

            entity
                .HasOne(a => a.App)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(p => p.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            entity
            .HasData(
                new AccountApp
                {
                    AccountId = AuthConstants.SEED_ACCOUNT_ID,
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                });
        });

        modelBuilder.Entity<RoleEfCore>(entity =>
        {
            entity
            .HasOne(r => r.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            entity
            .HasOne(r => r.App)
            .WithMany(a => a.Roles)
            .OnDelete(DeleteBehavior.Cascade);

            entity
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<RolePermission>();

            entity
            .HasData(
                new RoleEfCore
                {
                    Id = AuthConstants.TENANT_OWNER_ROLE_ID,
                    Name = "Tenant Owner",
                    Description = "Tenant Owner",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    IsPublic = true,
                    TenantId = seedTenantId,
                    IsDefault = false,
                },
                new RoleEfCore
                {
                    Id = AuthConstants.SEED_ROLE_ID,
                    Name = "Seed",
                    Description = "Should be deleted once used",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    IsPublic = false,
                    TenantId = seedTenantId,
                    IsDefault = false,
                },
                new RoleEfCore
                {
                    Id = AuthConstants.AUTH_WEB_API_OWNER_ROLE_ID,
                    Name = "Auth Provider Web Api Owner",
                    Description = "Permissions over Auth Provider Web Api app",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    IsPublic = true,
                    TenantId = seedTenantId,
                    IsDefault = false,
                },
                new RoleEfCore
                {
                    Id = appOwnerRoleId,
                    Name = "App owner",
                    Description = "App owner",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    IsPublic = true,
                    TenantId = seedTenantId,
                    IsDefault = false,
                },
                new RoleEfCore
                {
                    Id = clientOwnerRoleId,
                    Name = "Client owner",
                    Description = "Owner of an app that is client of other App",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    IsPublic = true,
                    TenantId = seedTenantId,
                    IsDefault = false,
                });
        });

        var createPermissionPermissionId = Guid.Parse("32b32564-459f-4e74-8456-83147bd03c9e");
        var getAllPermissionsPermissionId = Guid.Parse("bcb925af-f4be-4782-978c-c496b044609f");
        var createPermissionBulkPermissionId = Guid.Parse("e38a9a3a-dea3-46d5-a7a8-d5e9ea882e15");

        var createRolePermissionId = Guid.Parse("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9");
        var getAllRolesPermissionId = Guid.Parse("fc598ab0-1f14-4224-a187-4556a9926f6f");
        var addPermissionsPermissionId = Guid.Parse("c402e13f-40c4-4b97-b004-d5e616c3f82d");

        var createInvitationPermissionId = Guid.Parse("0b2f5e97-42f9-4e56-9ee2-40b033cff9e8");
        var getAllInvitationsPermissionId = Guid.Parse("40bc0960-8c55-488e-a014-f5b52db3d306");

        var createTenantPermissionid = Guid.Parse("45104ffc-433c-42bc-a887-18d71d2bc524");
        var getAllTenantsPermissionId = Guid.Parse("216b14a3-337a-45a6-a75d-cae870a73918");
        var patchTenantOwnerPermissionId = Guid.Parse("a43d40d7-7aa6-4abb-a124-890d7218ac86");
        var patchTenantNamePermissionId = Guid.Parse("06f5a862-9cfd-4c1f-a777-4c4b3adb916b");

        var getAllAccountsPermissionId = Guid.Parse("27c1378d-39df-4a57-b025-fc96963955a6");
        var createCredentialsForPermissionId = Guid.Parse("046c65a8-d3c1-41d7-bda2-a96d393cc18e");
        var updateRolesOfAccountPermissionId = Guid.Parse("c0a55e4b-b24d-42a4-90e4-f828e2b8e098");

        var createAppPermissionId = Guid.Parse("2eab3c3a-792a-444a-97f3-01db00dffcab");
        var getAllAppsPermissionId = Guid.Parse("6323b5da-b78c-4984-a56e-8206775d3e91");
        var updateColorsOfAppPermissionId = Guid.Parse("cfd3f238-a446-4f4f-81f0-f770974f0cc3");

        var createClientPermissionId = Guid.Parse("87013d07-c8ba-48f1-bb8c-510b7836fe1f");
        var getAllClientsPermissionId = Guid.Parse("43da8440-39be-46cc-b8fe-da34961d2486");

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId });

            entity
                .HasOne(p => p.Permission)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(p => p.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
            #region Seed
                new RolePermission
                {
                    RoleId = AuthConstants.SEED_ROLE_ID,
                    PermissionId = createCredentialsForPermissionId,
                },
                new RolePermission
                {
                    RoleId = AuthConstants.SEED_ROLE_ID,
                    PermissionId = getAllRolesPermissionId,
                },
            #endregion Seed

            #region Tenant Owner
            #region Permission
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createPermissionPermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = getAllPermissionsPermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createPermissionBulkPermissionId
                },
            #endregion Permission

            #region Role
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createRolePermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = getAllRolesPermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = addPermissionsPermissionId,
                },
            #endregion Role

            #region Invitation
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createInvitationPermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = getAllInvitationsPermissionId
                },
            #endregion Invitation

            #region Tenant
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createTenantPermissionid
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = patchTenantNamePermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = patchTenantOwnerPermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.AUTH_WEB_API_OWNER_ROLE_ID,
                    PermissionId = getAllTenantsPermissionId
                },
            #endregion Tenant

            #region Account
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = getAllAccountsPermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createCredentialsForPermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = updateRolesOfAccountPermissionId
                },
            #endregion Account

            #region App
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createAppPermissionId
                },
                new RolePermission
                {
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = getAllAppsPermissionId
                },
            #endregion App
            #endregion Tenant Owner

            #region App Owner
                new RolePermission
                {
                    RoleId = appOwnerRoleId,
                    PermissionId = updateColorsOfAppPermissionId
                },
                new RolePermission
                {
                    RoleId = appOwnerRoleId,
                    PermissionId = createRolePermissionId
                },
                new RolePermission
                {
                    RoleId = appOwnerRoleId,
                    PermissionId = getAllRolesPermissionId
                },
                new RolePermission
                {
                    RoleId = appOwnerRoleId,
                    PermissionId = createClientPermissionId
                },
                new RolePermission
                {
                    RoleId = appOwnerRoleId,
                    PermissionId = getAllClientsPermissionId
                },
                new RolePermission
                {
                    RoleId = appOwnerRoleId,
                    PermissionId = createCredentialsForPermissionId
                },
                #endregion App Owner

                #region Client Owner
                new RolePermission
                {
                    RoleId = clientOwnerRoleId,
                    PermissionId = updateColorsOfAppPermissionId
                },
                new RolePermission
                {
                    RoleId = clientOwnerRoleId,
                    PermissionId = createRolePermissionId
                },
                new RolePermission
                {
                    RoleId = clientOwnerRoleId,
                    PermissionId = getAllRolesPermissionId
                },
                new RolePermission
                {
                    RoleId = clientOwnerRoleId,
                    PermissionId = createCredentialsForPermissionId
                }
                #endregion Client Owner
            );
        });

        modelBuilder.Entity<PermissionEfCore>(entity =>
        {
            entity
            .HasOne(p => p.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            entity
            .HasOne(p => p.App)
            .WithMany(a => a.Permissions)
            .OnDelete(DeleteBehavior.Cascade);

            entity
            .HasMany(r => r.Roles)
            .WithMany(p => p.Permissions)
            .UsingEntity<RolePermission>();

            entity.HasData(
            #region Permission
                new PermissionEfCore
                {
                    Id = createPermissionPermissionId,
                    Name = "Create permission",
                    Description = "Can create permissions",
                    Key = "create-permission",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllPermissionsPermissionId,
                    Name = "Can read permissions",
                    Description = "Can read permissions",
                    Key = "getall-permission",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = createPermissionBulkPermissionId,
                    Name = "Create permission in bulk",
                    Description = "Can create several permissions at once",
                    Key = "createbulk-permission",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
            #endregion Permission

            #region Role
                new PermissionEfCore
                {
                    Id = createRolePermissionId,
                    Name = "Can create role",
                    Description = "Can create roles",
                    Key = "create-role",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllRolesPermissionId,
                    Name = "Can read roles",
                    Description = "Can read roles",
                    Key = "getall-role",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = addPermissionsPermissionId,
                    Name = "Can add permissions to role",
                    Description = "Can add permissions to role",
                    Key = "addpermission-role",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
            #endregion Role

            #region Invitation
                new PermissionEfCore
                {
                    Id = getAllInvitationsPermissionId,
                    Name = "Can read invitations of tenant",
                    Description = "Can read invitations of tenant",
                    Key = "getall-invitation",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },

                new PermissionEfCore
                {
                    Id = createInvitationPermissionId,
                    Name = "Can create invitations",
                    Description = "Can create invitations",
                    Key = "create-invitation",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
            #endregion Invitation

            #region Tenant
                new PermissionEfCore
                {
                    Id = createTenantPermissionid,
                    Name = "Can create tenant",
                    Description = "Can create tenant",
                    Key = "create-tenant",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = patchTenantNamePermissionId,
                    Name = "Can update tenant name",
                    Description = "Can update tenant name",
                    Key = "updatetenantname-me",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = patchTenantOwnerPermissionId,
                    Name = "Can update tenant owner",
                    Description = "Can update tenant owner",
                    Key = "transfertenant-me",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllTenantsPermissionId,
                    Name = "Can read all tenants",
                    Description = "Can read all tenants",
                    Key = "getall-tenants",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
            #endregion Tenant

            #region App
                new PermissionEfCore
                {
                    Id = createAppPermissionId,
                    Name = "Can create app",
                    Description = "Can create app",
                    Key = "create-app",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllAppsPermissionId,
                    Name = "Can read apps",
                    Description = "Can read apps of tenant",
                    Key = "getall-app",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = updateColorsOfAppPermissionId,
                    Name = "Can update colors of app",
                    Description = "Can update colors of app in tenant",
                    Key = "updatecolors-app",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
            #endregion App

            #region Client
                new PermissionEfCore
                {
                    Id = getAllClientsPermissionId,
                    Name = "Can read clients",
                    Description = "Can read clients of tenant",
                    Key = "getall-client",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = createClientPermissionId,
                    Name = "Can create clients",
                    Description = "Can create clients of tenant",
                    Key = "create-client",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
            #endregion

            #region Account
                new PermissionEfCore
                {
                    Id = getAllAccountsPermissionId,
                    Name = "Can read all accounts",
                    Description = "Can read all accounts",
                    Key = "getall-account",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = createCredentialsForPermissionId,
                    Name = "Can create accounts",
                    Description = "Can create accounts",
                    Key = "createcredentialsfor-account",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = updateRolesOfAccountPermissionId,
                    Name = "Update roles of account",
                    Description = "Update roles of account. Roles of tenant and of apps of user logged",
                    Key = "updateroles-account",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                }
            );
            #endregion Account
        });

        modelBuilder.Entity<SessionEfCore>(entity =>
        {
            entity
            .HasOne(s => s.App)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InvitationEfCore>(entity =>
        {
            entity
            .HasOne(i => i.Role)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            entity
            .HasOne(i => i.App)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

            entity
            .HasOne(i => i.Tenant)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
