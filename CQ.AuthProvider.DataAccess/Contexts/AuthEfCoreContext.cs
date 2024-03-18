using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
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

        public DbSet<ClientSystem> ClientSystems { get; set; }

        public AuthEfCoreContext(DbContextOptions<AuthEfCoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var accountId = "d4702564-8273-495b-a694-82fcc69da874";

            #region Permissions
            #region Permission permissions
            var createPermissionPermission = new PermissionEfCore
            (
                "Crear permiso",
                "Crear permiso",
                PermissionKey.CreatePermission,
                false);

            var createBulkPermissionPermission = new PermissionEfCore
            (
                "Crear muchos permisos",
                "Crear muchos permisos",
                PermissionKey.CreateBulkPermission,
                false);

            var getByIdPermissionPermission = new PermissionEfCore(
                "Obtener un permiso",
                "Obtener un permiso",
                PermissionKey.GetByIdPermission,
                false);

            var getAllPermissionsPermission = new PermissionEfCore(
                "Obtener permisos",
                "Obtener todos los permisos",
                PermissionKey.GetAllPermissions,
                false);

            var getAllPrivatePermissionsPermission = new PermissionEfCore(
                "Obtener permisos privados",
                "Obtener permisos privados",
                PermissionKey.GetAllPrivatePermissions,
                false);

            var getAllByRoleIdPermissionsPermission = new PermissionEfCore(
                "Obtener permisos de un rol",
                "Obtener permisos de un rol",
                PermissionKey.GetAllPermissionsByRoleId,
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
                PermissionKey.CreateRole,
                false);

            var createBulkRolePermission = new PermissionEfCore
            (
                "Crear muchos roles",
                "Crear muchos roles",
                PermissionKey.CreateBulkRole,
                false);

            var getByIdRolPermission = new PermissionEfCore(
                "Obtener un rol",
                "Obtener un rol",
                PermissionKey.GetByIdRole,
                false);

            var getAllRolesPermission = new PermissionEfCore(
                "Obtener roles",
                "Obtener todos los roles",
                PermissionKey.GetAllRoles,
                false);

            var getAllPrivateRolesPermission = new PermissionEfCore(
                "Obtener roles privados",
                "Obtener roles privados",
                PermissionKey.GetAllPrivateRoles,
                false);

            var updateByIdRolPermission = new PermissionEfCore(
                "Actualizar un rol",
                "Actualizar un rol",
                PermissionKey.AddPermissionToRole,
                false);
            #endregion

            #region ClientSystem permissions
            var createClientSystemPermission = new PermissionEfCore(
                "Crear client system",
                "Crear client system",
                PermissionKey.CreateClientSystem,
                false);

            var createAccountForPermission = new PermissionEfCore(
                "Crear cuenta para un usuario",
                "Crear cuenta para un usuario",
                PermissionKey.CreateAccountFor,
                false);
            #endregion

            var jokerPermission = new PermissionEfCore(
                "Joker",
                "Joker",
                PermissionKey.Joker,
                false);
            #endregion

            #region Roles
            var adminRole = new RoleEfCore(
                "Admin",
                "Admin",
                RoleKey.Admin,
                new List<PermissionEfCore>(),
                false);

            var clientSystemRole = new RoleEfCore(
                "Client System",
                "Client System",
                RoleKey.ClientSystem,
                new List<PermissionEfCore>(),
                false);
            #endregion

            var account = new AccountEfCore
            {
                Id = accountId,
                Email = "admin@gmail.com",
                FullName = "Admin Admin",
                FirstName = "Admin",
                LastName= "Admin"
            };

            modelBuilder
                .Entity<AccountEfCore>()
                .HasMany(a => a.Roles)
                .WithMany(r => r.Accounts)
                .UsingEntity<AccountRole>(
                r => r.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                l => l.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId)
                )
                .HasData(
                new AccountRole(
                    accountId,
                    adminRole.Id)
                );

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
                (adminRole.Id, getByIdPermissionPermission.Id),
                new RolePermission
                (adminRole.Id, getAllPermissionsPermission.Id),
                new RolePermission
                (adminRole.Id, getAllPrivatePermissionsPermission.Id),
                new RolePermission
                (adminRole.Id, getAllByRoleIdPermissionsPermission.Id),
                new RolePermission
                (adminRole.Id, updateByIdPermissionPermission.Id),
                new RolePermission
                (adminRole.Id, createRolePermission.Id),
                new RolePermission
                (adminRole.Id, getByIdRolPermission.Id),
                new RolePermission
                (adminRole.Id, getAllRolesPermission.Id),
                new RolePermission
                (adminRole.Id, getAllPrivateRolesPermission.Id),
                new RolePermission
                (adminRole.Id, updateByIdRolPermission.Id),
                new RolePermission
                (adminRole.Id, createClientSystemPermission.Id),
                new RolePermission
                (adminRole.Id, createAccountForPermission.Id),
                new RolePermission
                (clientSystemRole.Id, createBulkPermissionPermission.Id),
                new RolePermission
                (clientSystemRole.Id, createBulkRolePermission.Id));

            modelBuilder.Entity<PermissionEfCore>()
                .HasData(
                createPermissionPermission,
                createBulkPermissionPermission,
                getByIdPermissionPermission,
                getAllPermissionsPermission,
                getAllPrivatePermissionsPermission,
                getAllByRoleIdPermissionsPermission,
                updateByIdPermissionPermission,
                createRolePermission,
                createBulkRolePermission,
                getByIdRolPermission,
                getAllRolesPermission,
                getAllPrivateRolesPermission,
                updateByIdRolPermission,
                createClientSystemPermission,
                createAccountForPermission);

            modelBuilder.Entity<RoleEfCore>().HasData(
                adminRole,
                clientSystemRole);

            modelBuilder.Entity<AccountEfCore>().HasData(account);
        }
    }

    public sealed record class AccountRole
    {
        public string AccountId { get; set; } = null!;

        public string RoleId { get; set; } = null!;

        public AccountEfCore Account { get; set; } = null!;

        public RoleEfCore Role { get; set; } = null!;

        public AccountRole()
        {
        }

        public AccountRole(
            string accountId,
            string roleId)
        {
            this.AccountId = accountId;
            this.RoleId = roleId;
        }
    }
}
