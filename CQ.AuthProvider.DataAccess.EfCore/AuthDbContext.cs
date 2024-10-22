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
        const string seedTenantId = "b22fcf202bd84a97936ccf2949e00da4";
        const string seedAccountId = "5a0d9e179991499e80db0a15fda4df79";
        const string authWebApiOwnerRoleId = "dfa136595e304b98ad7b55d782c6a12c";
        const string seedRoleId = "0415b39e83cd4fbdb33c5004a0b65294";

        modelBuilder.Entity<TenantEfCore>(entity =>
        {
            entity
            .HasOne(t => t.Owner)
            .WithOne()
            .HasForeignKey<TenantEfCore>(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

            entity
            .Property(e => e.OwnerId)
            .HasMaxLength(450);

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
            .HasMany(a => a.Permissions)
            .WithOne(p => p.App)
            .OnDelete(DeleteBehavior.Cascade);

            entity
            .HasData(
                new AppEfCore
                {
                    Id = AuthConstants.AUTH_WEB_API_APP_ID,
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
            .UsingEntity<AccountRole>();

            entity
            .HasMany(a => a.Apps)
            .WithMany()
            .UsingEntity<AccountApp>();

            entity
            .HasOne(a => a.Tenant)
            .WithMany(t => t.Accounts)
            .HasForeignKey(a => a.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

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
                TenantId = seedTenantId,
                CreatedAt = new DateTime(2024, 1, 1),
            });
        });

        modelBuilder.Entity<AccountRole>(entity =>
        {
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
                    Id = "1f191c90510d456d84bda9e17fe24f50",
                    AccountId = seedAccountId,
                    RoleId = seedRoleId,
                })
            ;
        });

        modelBuilder.Entity<AccountApp>(entity =>
        {
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
                    Id = "ef03980ea2a54e4bba92e022fbd33d9b",
                    AccountId = seedAccountId,
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                })
            ;
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
                    Id = authWebApiOwnerRoleId,
                    Name = "Auth Web Api Owner",
                    Description = "Owner of Auth Web Api App",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    IsPublic = true,
                    TenantId = seedTenantId,
                    IsDefault = false,
                },
                new RoleEfCore
                {
                    Id = seedRoleId,
                    Name = "Seed",
                    Description = "Should be deleted once deployed",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    IsPublic = false,
                    TenantId = seedTenantId,
                    IsDefault = false,
                });
        });

        const string createPermissionPermissionId = "39d20cb8c4d541c6944aeeb678261cea";
        const string getAllPermissionsPermissionId = "d40ad347c7f943e399069eef409b4fa6";

        const string createRolePermissionId = "8b1c2d303f3b45a1aa3ae6af46c8652b";
        const string getAllRolesPermissionId = "aca002cfbf3a47899ff4c16e6be2c029";

        const string createInvitationPermissionId = "f3ba5c2342a248d89eee2977456d54cd";
        const string getAllInvitationsPermissionId = "80ca0e41ea5046519f351a99b03b294e";

        const string createTenantPermissionid = "9203d8a99d4e4a3b9b47f7db0e81353e";
        const string getAllTenantsPermissionId = "9b079f7461554950bbd981f929568322";
        const string patchTenantOwnerPermissionId = "91cc2fb3a90e4f4aa01c02a363ae44c3";
        const string patchTenantNamePermissionId = "7d21bd25e0b74951b06772ca348e81fa";

        const string getAllAccountsPermissionId = "33d7733f42214f6785e10a480c45a007";

        const string createAppPermissionId = "7e9af6ea241342c5bb97c634a36c2de2";
        const string getAllAppsPermissionId = "843aa6fb505b4f919930aeeea10511ee";

        modelBuilder.Entity<RolePermission>(entity =>
        {
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
                    Id = "ea84e710483e42eea573260151916d36",
                    RoleId = seedRoleId,
                    PermissionId = createInvitationPermissionId
                },
                new RolePermission
                {
                    Id = "2ea1bb330e3e489cbf3402daacef9905",
                    RoleId = seedRoleId,
                    PermissionId = getAllRolesPermissionId
                },
            #endregion

            #region Tenant Owner
            #region Permission
                new RolePermission
                {
                    Id = "c07081abf2054ec496bee67b44a2ee2a",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createPermissionPermissionId
                },
                new RolePermission
                {
                    Id = "d76afb762df349caadc39b7373ea98ed",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = getAllPermissionsPermissionId
                },
            #endregion

            #region Role
                new RolePermission
                {
                    Id = "be097c9f1b4e4b3088172bcb0c75372b",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createRolePermissionId
                },
                new RolePermission
                {
                    Id = "6e3f476ec4354b27af25e025034ee97e",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = getAllRolesPermissionId
                },
            #endregion

            #region Invitation
                new RolePermission
                {
                    Id = "c867b020844a4a6fa495b013bc903b3a",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createInvitationPermissionId
                },
                new RolePermission
                {
                    Id = "d26570a4df1a41fea0bf0006f1b87721",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = getAllInvitationsPermissionId
                },
            #endregion

            #region Tenant
                new RolePermission
                {
                    Id = "8c52753c02324daeb56fb4557c2eaf46",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createTenantPermissionid
                },
                new RolePermission
                {
                    Id = "60307119a6f8403faaf53606eceefedc",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = patchTenantNamePermissionId
                },
                new RolePermission
                {
                    Id = "20ba3bbf9e87433199a49bc01c928014",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = patchTenantOwnerPermissionId
                },
            #endregion

            #region App
                new RolePermission
                {
                    Id = "f368580391cc459c964ce099cebb9b02",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = createAppPermissionId
                },
                new RolePermission
                {
                    Id = "116fcf12e6aa43fa837dce2199ce195c",
                    RoleId = AuthConstants.TENANT_OWNER_ROLE_ID,
                    PermissionId = getAllAppsPermissionId
                },
            #endregion
            #endregion

            #region Auth Web Api Owner
                new RolePermission
                {
                    Id = "89c5ad347a8f41c0864a4a37f7be5224",
                    RoleId = authWebApiOwnerRoleId,
                    PermissionId = getAllTenantsPermissionId
                },
                new RolePermission
                {
                    Id = "16ef3304b62240b2bd86b4287f14bea3",
                    RoleId = authWebApiOwnerRoleId,
                    PermissionId = getAllAccountsPermissionId
                });
            #endregion
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
            #region Tenant Owner
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
            #endregion

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
            #endregion

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
            #endregion

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
            #endregion

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
            #endregion
            #endregion

            #region Auth Web Api Owner
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
                new PermissionEfCore
                {
                    Id = getAllAccountsPermissionId,
                    Name = "Can read all accounts",
                    Description = "Can read all accounts",
                    Key = "getall-accounts",
                    AppId = AuthConstants.AUTH_WEB_API_APP_ID,
                    TenantId = seedTenantId,
                    IsPublic = true,
                });
            #endregion
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
