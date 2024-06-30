using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.Utility;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class CreateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Tenants\"");
            migrationBuilder.Sql("DELETE FROM \"Accounts\"");
            migrationBuilder.Sql("DELETE FROM \"Roles\"");
            migrationBuilder.Sql("DELETE FROM \"Permissions\"");

            #region Auth Provider Tenant and Account
            migrationBuilder
                .DropForeignKey("FK_Tenants_Accounts_OwnerId", "Tenants");

            var ownerId = Db.NewId();
            var authProviderTenantId = Db.NewId();
            migrationBuilder
                .InsertData("Tenants",
                ["Id", "Name", "OwnerId"],
                [authProviderTenantId, "Auth Provider", ownerId]);

            migrationBuilder
                .InsertData("Accounts",
                ["Id", "FirstName", "LastName", "FullName", "Email", "Locale", "TimeZone", "TenantId", "ProfilePictureUrl", "CreatedAt"],
                [ownerId, "Admin", "Admin", "Admin Admin", "admin@gmail.com", "Uruguay", "UTC -3", authProviderTenantId, null, DateTimeOffset.UtcNow.Date]);

            migrationBuilder
                .AddForeignKey("FK_Tenants_Accounts_OwnerId", "Tenants", "OwnerId", "Accounts");
            #endregion

            #region Permissions
            #region Permission
            var createPermissionId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [createPermissionId, "Crear permisos", "Crear permisos para los endpoints", PermissionKey.CreatePermission.ToString(), false, null]);

            var createBulkPermissionId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [createBulkPermissionId, "Crear varios permisos", "Crear varios permisos para endpoints", PermissionKey.CreateBulkPermission.ToString(), false, null]);

            var getAllPermissionId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [getAllPermissionId, "Obtener todos los permisos publicos", "Obtener todos los permisos publicos", PermissionKey.GetAllPermissions.ToString(), false, null]);

            var getAllPrivatePermissionId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [getAllPrivatePermissionId, "Obtener permisos privados", "Obtener permisos privados", PermissionKey.GetAllPrivatePermissions.ToString(), false, null]);

            var getAllByRolePermissionId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [getAllByRolePermissionId, "Obtener permisos de rol", "Obtener permisos de rol", PermissionKey.GetAllPermissionsByRoleId.ToString(), false, null]);

            var getByIdPermissionId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [getByIdPermissionId, "Obtener un permiso", "Obtener un permiso", PermissionKey.GetByIdPermission.ToString(), false, null]);

            var updateByIdPermissionId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [updateByIdPermissionId, "Actualizar permiso", "Actualizar un permiso", PermissionKey.GetByIdPermission.ToString(), false, null]);
            #endregion

            #region Role
            var createRoleId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [createRoleId, "Crear rol", "Crear rol", PermissionKey.CreateRole.ToString(), false, null]);

            var createBulkRoleId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [createBulkRoleId, "Crear roles", "Crear roles", PermissionKey.CreateBulkRole.ToString(), false, null]);

            var getByIdRoleId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [getByIdRoleId, "Crear obtener rol", "Crear obtener rol", PermissionKey.GetByIdRole.ToString(), false, null]);

            var getAllRoleId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [getAllRoleId, "Obtener varios roles", "Obtener varios roles", PermissionKey.GetAllRoles.ToString(), false, null]);

            var getAllPrivateRoleId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [getAllPrivateRoleId, "Obtener roles privados", "Obtener roles privados", PermissionKey.GetAllPrivateRoles.ToString(), false, null]);

            var addPermissionToRoleId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [addPermissionToRoleId, "Agregar permisos a rol", "Agregar permisos a rol", PermissionKey.AddPermissionToRole.ToString(), false, null]);
            #endregion

            #region Invitation
            var createInvitationId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [createInvitationId, "Crear invitacion", "Crear invitacion", PermissionKey.CreateInvitation.ToString(), false, null]);

            var getAllInvitationId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [getAllInvitationId, "Obtener invitaciones", "Obtener invitaciones", PermissionKey.GetAllInvitation.ToString(), false, null]);
            #endregion

            var createAccountForId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [createAccountForId, "Crear cuenta para usuario", "Crear cuenta para usuario", PermissionKey.CreateAccountFor.ToString(), false, null]);

            var jokerId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "TenantId"],
                [jokerId, "Comodin", "Comodin", PermissionKey.Joker.ToString(), false, null]);
            #endregion

            #region Roles
            var adminRoleId = Db.NewId();
            migrationBuilder
                .InsertData("Roles",
                ["Id", "Name", "Description", "Key", "IsPublic", "IsDefault", "TenantId"],
                [adminRoleId, "Admin", "Admin", RoleKey.Admin.ToString(), false, false, null]);

            var tenantRoleId = Db.NewId();
            migrationBuilder
                .InsertData("Roles",
                ["Id", "Name", "Description", "Key", "IsPublic", "IsDefault", "TenantId"],
                [tenantRoleId, "Tenant", "Tenant", RoleKey.TenantOwner.ToString(), false, false, authProviderTenantId]);
            #endregion

            #region Apps
            var authProviderAppId = Db.NewId();
            migrationBuilder
                .InsertData("Apps",
                ["Id", "Name", "TenantId"],
                [authProviderAppId, "Auth Provider API", authProviderTenantId]);
            #endregion

            #region Connect accounts with roles
            migrationBuilder
                .InsertData("AccountsRoles",
                ["Id", "AccountId", "RoleId", "TenantId"],
                [Db.NewId(), ownerId, tenantRoleId, authProviderTenantId]);
            #endregion

            #region Connect roles with permissions
            migrationBuilder
                .InsertData("RolesPermissions",
                ["Id", "RoleId", "PermissionId", "TenantId"],
                [Db.NewId(), adminRoleId, jokerId, authProviderTenantId]);

            migrationBuilder
                .InsertData("RolesPermissions",
                ["Id", "RoleId", "PermissionId", "TenantId"],
                [Db.NewId(), tenantRoleId, jokerId, authProviderTenantId]);
            #endregion

            #region Connect accounts with apps
            migrationBuilder
                .InsertData("AccountsApps",
                ["Id", "AccountId", "AppId", "TenantId"],
                [Db.NewId(), ownerId, authProviderAppId, authProviderTenantId]);
            #endregion

            #region Connect roles with apps
            migrationBuilder
                .InsertData("RolesApps",
                ["Id", "RoleId", "AppId", "TenantId"],
                [Db.NewId(), adminRoleId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("RolesApps",
                ["Id", "RoleId", "AppId", "TenantId"],
                [Db.NewId(), tenantRoleId, authProviderAppId, authProviderTenantId]);
            #endregion

            #region Connect permissions with apps
            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), createPermissionId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), createBulkPermissionId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), getAllPermissionId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), getAllPrivatePermissionId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), getAllByRolePermissionId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), getByIdPermissionId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), updateByIdPermissionId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), createRoleId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), createBulkRoleId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), getAllRoleId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), getAllPrivateRoleId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), getByIdRoleId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), addPermissionToRoleId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), createInvitationId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), getAllInvitationId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), createAccountForId, authProviderAppId, authProviderTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), jokerId, authProviderAppId, authProviderTenantId]);
            #endregion
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Tenants\"");
            migrationBuilder.Sql("DELETE FROM \"Accounts\"");
            migrationBuilder.Sql("DELETE FROM \"AccountsApps\"");
            migrationBuilder.Sql("DELETE FROM \"AccountsRoles\"");
            migrationBuilder.Sql("DELETE FROM \"Roles\"");
            migrationBuilder.Sql("DELETE FROM \"RolesApps\"");
            migrationBuilder.Sql("DELETE FROM \"RolesPermissions\"");
            migrationBuilder.Sql("DELETE FROM \"Permissions\"");
            migrationBuilder.Sql("DELETE FROM \"PermissionsApps\"");
        }
    }
}
