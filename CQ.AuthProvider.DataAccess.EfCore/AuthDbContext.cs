using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Sessions;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore;

public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options)
    : EfCoreContext(options)
{
    public DbSet<AccountEfCore> Accounts { get; set; }

    public DbSet<AccountRole> AccountsRoles { get; set; }

    public DbSet<RoleEfCore> Roles { get; set; }

    public DbSet<RolePermission> RolesPermissions { get; set; }

    public DbSet<PermissionEfCore> Permissions { get; set; }

    public DbSet<SessionEfCore> Sessions { get; set; }

    public DbSet<ResetPasswordEfCore> ResetPasswords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
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
            (adminRole.Id, createBulkPermissionPermission.Id),
            new RolePermission
            (adminRole.Id, createBulkRolePermission.Id),

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
