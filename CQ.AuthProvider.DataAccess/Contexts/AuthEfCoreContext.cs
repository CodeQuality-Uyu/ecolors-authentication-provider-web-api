using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.DataAccess.AppConfig;
using CQ.AuthProvider.EfCore;
using CQ.UnitOfWork.EfCore;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

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

        public DbSet<ClientSystemEfCore> ClientSystems { get; set; }

        public AuthEfCoreContext(DbContextOptions<AuthEfCoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var (accountBuilder, roleBuilder) = ConfigStructure(modelBuilder);

            SeedData(accountBuilder, roleBuilder, modelBuilder);
        }

        private (EntityTypeBuilder<AccountRole>, EntityTypeBuilder<RolePermission>) ConfigStructure(ModelBuilder modelBuilder)
        {
            var accountBuilder = modelBuilder
               .Entity<AccountEfCore>()
               .HasMany(a => a.Roles)
               .WithMany(r => r.Accounts)
               .UsingEntity<AccountRole>(
               r => r.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
               l => l.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.AccountId));

            var roleBuilder = modelBuilder
                .Entity<RoleEfCore>()
                .HasMany(a => a.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>(
                r => r.HasOne(x => x.Permission).WithMany().HasForeignKey(x => x.PermissionId),
                l => l.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId));

            return (accountBuilder, roleBuilder);
        }

        private void SeedData(
            EntityTypeBuilder<AccountRole> accountBuilder,
            EntityTypeBuilder<RolePermission> roleBuilder,
            ModelBuilder modelBuilder)
        {

            #region Permissions
            #region Permission permissions
            var createPermissionPermission = new PermissionEfCore
            (
                "Crear permiso",
                "Crear permiso",
                PermissionKey.CreatePermission);

            var createBulkPermissionPermission = new PermissionEfCore
            (
                "Crear muchos permisos",
                "Crear muchos permisos",
                PermissionKey.CreateBulkPermission);

            var getByIdPermissionPermission = new PermissionEfCore(
                "Obtener un permiso",
                "Obtener un permiso",
                PermissionKey.GetByIdPermission);

            var getAllPermissionsPermission = new PermissionEfCore(
                "Obtener permisos",
                "Obtener todos los permisos",
                PermissionKey.GetAllPermissions);

            var getAllPrivatePermissionsPermission = new PermissionEfCore(
                "Obtener permisos privados",
                "Obtener permisos privados",
                PermissionKey.GetAllPrivatePermissions);

            var getAllByRoleIdPermissionsPermission = new PermissionEfCore(
                "Obtener permisos de un rol",
                "Obtener permisos de un rol",
                PermissionKey.GetAllPermissionsByRoleId);

            var updateByIdPermissionPermission = new PermissionEfCore(
                "Actualizar un permiso",
                "Actualizar un permiso",
                new PermissionKey("updatebyid-permission"));
            #endregion

            #region Role permissions
            var createRolePermission = new PermissionEfCore
            (
                "Crear rol",
                "Crear rol",
                PermissionKey.CreateRole);

            var createBulkRolePermission = new PermissionEfCore
            (
                "Crear muchos roles",
                "Crear muchos roles",
                PermissionKey.CreateBulkRole);

            var getByIdRolPermission = new PermissionEfCore(
                "Obtener un rol",
                "Obtener un rol",
                PermissionKey.GetByIdRole);

            var getAllRolesPermission = new PermissionEfCore(
                "Obtener roles",
                "Obtener todos los roles",
                PermissionKey.GetAllRoles);

            var getAllPrivateRolesPermission = new PermissionEfCore(
                "Obtener roles privados",
                "Obtener roles privados",
                PermissionKey.GetAllPrivateRoles);

            var updateByIdRolPermission = new PermissionEfCore(
                "Actualizar un rol",
                "Actualizar un rol",
                PermissionKey.AddPermissionToRole);
            #endregion

            #region ClientSystem permissions
            var createClientSystemPermission = new PermissionEfCore(
                "Crear client system",
                "Crear client system",
                PermissionKey.CreateClientSystem);

            var createAccountForPermission = new PermissionEfCore(
                "Crear cuenta para un usuario",
                "Crear cuenta para un usuario",
                PermissionKey.CreateAccountFor);

            var validateTokenPermission = new PermissionEfCore(
                "Validar token",
                "Validar token",
                PermissionKey.ValidateToken);
            #endregion

            var jokerPermission = new PermissionEfCore(
                "Joker",
                "Joker",
                PermissionKey.Joker);
            #endregion

            #region Roles
            var adminRole = new RoleEfCore(
                "Admin",
                "Admin",
                RoleKey.Admin,
                new List<PermissionEfCore>());

            var clientSystemRole = new RoleEfCore(
                "Client System",
                "Client System",
                RoleKey.ClientSystem,
                new List<PermissionEfCore>());
            #endregion

            var account = new AccountEfCore
            {
                Id = IdentityProviderEfCoreContext.ADMIN_ID,
                Email = IdentityProviderEfCoreContext.ADMIN_EMAIL,
                FullName = "Admin Admin",
                FirstName = "Admin",
                LastName = "Admin"
            };

            accountBuilder.HasData(
                new AccountRole(
                    account.Id,
                    adminRole.Id));

            roleBuilder.HasData(
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
                (clientSystemRole.Id, createBulkRolePermission.Id),
                new RolePermission
                (clientSystemRole.Id, validateTokenPermission.Id));

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
                createAccountForPermission,
                jokerPermission,
                validateTokenPermission);

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
