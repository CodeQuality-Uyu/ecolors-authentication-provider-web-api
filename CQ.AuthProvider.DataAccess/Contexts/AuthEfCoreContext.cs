using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.UnitOfWork.EfCore;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.Contexts
{
    public sealed class AuthEfCoreContext : EfCoreContext
    {
        public DbSet<AccountEfCore> Accounts { get; set; }

        public DbSet<AccountRole> AccountsRoles { get; set; }

        public DbSet<RoleEfCore> Roles { get; set; }

        public DbSet<RolePermission> RolesPermissions { get; set; }

        public DbSet<PermissionEfCore> Permissions { get; set; }

        public DbSet<ResetPasswordApplicationEfCore> ResetPasswordApplications { get; set; }

        public AuthEfCoreContext(DbContextOptions<AuthEfCoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var accountId = "d4702564-8273-495b-a694-82fcc69da874";

            #region Permission permissions
            var createPermissionPermission = new PermissionEfCore
            (
                "Crear permiso",
                "Crear permiso",
                new PermissionKey("create-permission"),
                false);

            var getByIdPermissionPermission = new PermissionEfCore(
                "Obtener un permiso",
                "Obtener un permiso",
                new PermissionKey("getbyid-permission"),
                false);

            var getAllPermissionsPermission = new PermissionEfCore(
                "Obtener permisos",
                "Obtener todos los permisos",
                new PermissionKey("getall-permission"),
                false);

            var getAllPublicPermissionsPermission = new PermissionEfCore(
                "Obtener permisos publicos",
                "Obtener permisos publicos",
                new PermissionKey("getprivate-permission"),
                false);

            var updateByIdPermissionPermission = new PermissionEfCore(
                "Actualizar un permiso",
                "Actualizar un permiso",
                new PermissionKey("updatebyid-permission"),
                false);
            #endregion

            #region Role permissions
            var createRolePermission = new PermissionEfCore
            (
                "Crear rol",
                "Crear rol",
                new PermissionKey("create-role"),
                false);

            var getByIdRolPermission = new PermissionEfCore(
                "Obtener un rol",
                "Obtener un rol",
                new PermissionKey("getbyid-role"),
                false);

            var getAllRolesPermission = new PermissionEfCore(
                "Obtener roles",
                "Obtener todos los roles",
                new PermissionKey("getall-role"),
                false);

            var getAllPublicRolesPermission = new PermissionEfCore(
                "Obtener roles publicos",
                "Obtener roles publicos",
                PermissionKey.GetPrivateRoles,
                false);

            var updateByIdRolPermission = new PermissionEfCore(
                "Actualizar un rol",
                "Actualizar un rol",
                new PermissionKey("addpermission-role"),
                false);
            #endregion

            var adminRole = new RoleEfCore(
                "Admin",
                "Admin",
                new RoleKey("admin"),
                new List<PermissionEfCore>(),
                false);

            var account = new AccountEfCore
            {
                Id = accountId,
                Email = "admin@gmail.com",
                Name = "Admin Admin"
            };

            modelBuilder
                .Entity<AccountEfCore>()
                .HasMany(a => a.Roles)
                .WithMany(r => r.Accounts)
                .UsingEntity<AccountRole>(
                r => r.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                l => l.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId)
                )
                .HasData(new AccountRole
                {
                    AccountId = accountId,
                    RoleId = adminRole.Id,
                });

            modelBuilder
                .Entity<RoleEfCore>()
                .HasMany(a => a.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>(
                r => r.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId),
                l => l.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId)
                ).HasData(

                new RolePermission
                (adminRole.Id, createPermissionPermission.Id),
                new RolePermission
                (adminRole.Id, getAllPermissionsPermission.Id),
                new RolePermission
                (adminRole.Id, getByIdPermissionPermission.Id),
                new RolePermission
                (adminRole.Id, getAllPublicPermissionsPermission.Id),
                new RolePermission
                (adminRole.Id, updateByIdPermissionPermission.Id),
                new RolePermission
                (adminRole.Id, createRolePermission.Id),
                new RolePermission
                (adminRole.Id, getAllRolesPermission.Id),
                new RolePermission
                (adminRole.Id, getAllPublicRolesPermission.Id),
                new RolePermission
                (adminRole.Id, getByIdRolPermission.Id),
                new RolePermission
                (adminRole.Id, updateByIdRolPermission.Id));

            modelBuilder.Entity<PermissionEfCore>()
                .HasData(
                getByIdPermissionPermission,
                getAllPermissionsPermission,
                getAllPublicPermissionsPermission,
                createPermissionPermission,
                updateByIdPermissionPermission,
                createRolePermission,
                getByIdRolPermission,
                getAllRolesPermission,
                getAllPublicRolesPermission,
                updateByIdRolPermission);
            modelBuilder.Entity<RoleEfCore>().HasData(adminRole);
            modelBuilder.Entity<AccountEfCore>().HasData(account);
        }
    }

    public sealed record class AccountRole
    {
        public string AccountId { get; set; } = null!;

        public string RoleId { get; set; } = null!;

        public AccountEfCore Account { get; set; } = null!;

        public RoleEfCore Role { get; set; } = null!;
    }

    public sealed record class RolePermission
    {
        public string RoleId { get; set; } = null!;

        public string PermissionId { get; set; } = null!;

        public RoleEfCore Role { get; set; } = null!;

        public PermissionEfCore Permission { get; set; } = null!;

        public RolePermission()
        {
        }

        public RolePermission(
            string roleId,
            string permissionId)
        {
            this.RoleId = roleId;
            this.PermissionId = permissionId;
        }
    }
}
