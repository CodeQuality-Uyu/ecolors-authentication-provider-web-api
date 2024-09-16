using CQ.AuthProvider.BusinessLogic.Permissions;
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
    public const string SEED_TENANT_ID = "b22fcf202bd84a97936ccf2949e00da4";
    public const string AUTH_WEB_API_APP_ID = "d31184dabbc6435eaec86694650c2679";

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
        const string tenantOwnerRoleId = "5c2260fc58864b75a4cad5c0e7dd57cb";
        const string authWebApiOwnerRoleId = "dfa136595e304b98ad7b55d782c6a12c";

        modelBuilder.Entity<TenantEfCore>(entity =>
        {
            entity
            .HasOne(t => t.Owner)
            .WithOne(o => o.Tenant)
            .HasForeignKey<TenantEfCore>(t => t.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

            entity
            .Property(e => e.OwnerId)
            .HasMaxLength(450);

            entity
            .HasData(
                new TenantEfCore
                {
                    Id = SEED_TENANT_ID,
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
                    Id = AUTH_WEB_API_APP_ID,
                    Name = "Auth Provider WEB API",
                    IsDefault = true,
                    TenantId = SEED_TENANT_ID,
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
            .WithOne(t => t.Owner)
            .HasForeignKey<AccountEfCore>(a => a.TenantId);

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
                TenantId = SEED_TENANT_ID,
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
                    RoleId = tenantOwnerRoleId,
                },
                new AccountRole
                {
                    Id = "735eeea5c936492794514b5cc3ecd217",
                    AccountId = seedAccountId,
                    RoleId = authWebApiOwnerRoleId,
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
                    AppId = AUTH_WEB_API_APP_ID,
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
                    Id = tenantOwnerRoleId,
                    Name = "Tenant Owner",
                    Description = "Tenant Owner",
                    AppId = AUTH_WEB_API_APP_ID,
                    IsPublic = true,
                    TenantId = SEED_TENANT_ID,
                    IsDefault = false,
                },
                new RoleEfCore
                {
                    Id = authWebApiOwnerRoleId,
                    Name = "Auth Web Api Owner",
                    Description = "Owner of Auth Web Api App",
                    AppId = AUTH_WEB_API_APP_ID,
                    IsPublic = true,
                    TenantId = SEED_TENANT_ID,
                    IsDefault = false,
                });
        });

        const string createPermissionPermissionId = "39d20cb8c4d541c6944aeeb678261cea";
        const string getAllPermissionsPermissionId = "d40ad347c7f943e399069eef409b4fa6";
        const string getAllPermissionsOfTenantPermissionId = "d1d34f71201f4b3e8f1c232aef35c40a";
        const string getAllPrivatePermissionsPermissionId = "e0132221c91f44ada257a38d951407d6";
        const string getAllPermissionsOfRolePermissionId = "05276f2a25dc4db5b37b0948e05c35ab";

        const string createRolePermissionId = "8b1c2d303f3b45a1aa3ae6af46c8652b";
        const string getAllRolesPermissionId = "aca002cfbf3a47899ff4c16e6be2c029";
        const string getAllRolesOfTenantPermissionId = "1554a06426024ee88baabad7a71d65cf";
        const string getAllPrivateRolesPermissionId = "1ce9908dba38490cbc65389bfeece21e";

        const string getAllInvitationsPermissionId = "80ca0e41ea5046519f351a99b03b294e";

        const string getAllTenantsPermissionId = "9b079f7461554950bbd981f929568322";
        const string createTenantPermissionid = "9203d8a99d4e4a3b9b47f7db0e81353e";
        const string patchTenantNamePermissionId = "7d21bd25e0b74951b06772ca348e81fa";
        const string patchTenantOwnerPermissionId = "91cc2fb3a90e4f4aa01c02a363ae44c3";

        const string getAllAccountsPermissionId = "33d7733f42214f6785e10a480c45a007";

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
                #region Tenant Owner
                #region Permission
                new RolePermission
                {
                    Id = "c07081abf2054ec496bee67b44a2ee2a",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = createPermissionPermissionId
                },
                new RolePermission
                {
                    Id = "d76afb762df349caadc39b7373ea98ed",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = getAllPermissionsPermissionId
                },
                new RolePermission
                {
                    Id = "e568c9eb81b24a5cae922c2a9a2ebc41",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = getAllPermissionsOfTenantPermissionId
                },
                new RolePermission
                {
                    Id = "8d01aeac30dd45599da743bcc3f3ee0d",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = getAllPrivatePermissionsPermissionId
                },
                new RolePermission
                {
                    Id = "a85e6d40858e4451b8a103bd903b6269",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = getAllPermissionsOfRolePermissionId
                },
            #endregion
                
                #region Role
                new RolePermission
                {
                    Id = "be097c9f1b4e4b3088172bcb0c75372b",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = createRolePermissionId
                },
                new RolePermission
                {
                    Id = "6e3f476ec4354b27af25e025034ee97e",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = getAllRolesPermissionId
                },
                new RolePermission
                {
                    Id = "fe7a5b81f9284af1857621c234ebc615",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = getAllRolesOfTenantPermissionId
                },
                new RolePermission
                {
                    Id = "922a41f8597742178605a5ea7c75be32",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = getAllPrivateRolesPermissionId
                },
            #endregion
                
                #region Invitation
                new RolePermission
                {
                    Id = "d26570a4df1a41fea0bf0006f1b87721",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = getAllInvitationsPermissionId
                },
            #endregion
                
                #region Tenant
                new RolePermission
                {
                    Id = "8c52753c02324daeb56fb4557c2eaf46",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = createTenantPermissionid
                },
                new RolePermission
                {
                    Id = "60307119a6f8403faaf53606eceefedc",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = patchTenantNamePermissionId
                },
                new RolePermission
                {
                    Id = "20ba3bbf9e87433199a49bc01c928014",
                    RoleId = tenantOwnerRoleId,
                    PermissionId = patchTenantOwnerPermissionId
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
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllPermissionsPermissionId,
                    Name = "Can read permissions",
                    Description = "Can read permissions",
                    Key = "getall-permission",
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllPermissionsOfTenantPermissionId,
                    Name = "Can read permissions of tenant",
                    Description = "Can read permissions of tenant",
                    Key = PermissionKey.CanReadPermissionsOfTenant,
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = false,
                },
                new PermissionEfCore
                {
                    Id = getAllPrivatePermissionsPermissionId,
                    Name = "Can read private permissions",
                    Description = "Can read private permissions",
                    Key = PermissionKey.CanReadPrivatePermissions,
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = false,
                },
                new PermissionEfCore
                {
                    Id = getAllPermissionsOfRolePermissionId,
                    Name = "Can filter permissions by role",
                    Description = "Can filter permissions by role",
                    Key = PermissionKey.CanReadPermissionsOfRole,
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = false,
                },
            #endregion

                #region Role
                new PermissionEfCore
                {
                    Id = createRolePermissionId,
                    Name = "Can create role",
                    Description = "Can create roles",
                    Key = "create-role",
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllRolesPermissionId,
                    Name = "Can read roles",
                    Description = "Can read roles",
                    Key = "getall-role",
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllRolesOfTenantPermissionId,
                    Name = "Can read roles of tenant",
                    Description = "Can read roles of tenant",
                    Key = PermissionKey.CanReadRolesOfTenant,
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllPrivateRolesPermissionId,
                    Name = "Can read private roles",
                    Description = "Can read private roles",
                    Key = PermissionKey.CanReadPrivateRoles,
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = false,
                },
            #endregion

                #region Invitation
                new PermissionEfCore
                {
                    Id = getAllInvitationsPermissionId,
                    Name = "Can read invitations of tenant",
                    Description = "Can read invitations of tenant",
                    Key = "getall-invitation",
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = true,
                },
            #endregion
            
                #region Tenant
                new PermissionEfCore
                {
                    Id = createTenantPermissionid,
                    Name = "Can create tenant",
                    Description = "Can create tenant",
                    Key = "create-tenants",
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = patchTenantNamePermissionId,
                    Name = "Can update tenant name",
                    Description = "Can update tenant name",
                    Key = "patchname-tenants",
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = patchTenantOwnerPermissionId,
                    Name = "Can update tenant owner",
                    Description = "Can update tenant owner",
                    Key = "patchowner-tenants",
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
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
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
                    IsPublic = true,
                },
                new PermissionEfCore
                {
                    Id = getAllAccountsPermissionId,
                    Name = "Can read all accounts",
                    Description = "Can read all accounts",
                    Key = "getall-accounts",
                    AppId = AUTH_WEB_API_APP_ID,
                    TenantId = SEED_TENANT_ID,
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
    }
}
