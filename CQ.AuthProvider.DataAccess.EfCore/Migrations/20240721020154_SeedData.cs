using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.Utility;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            #region Auth Provider Tenant and Account
            migrationBuilder
                .DropForeignKey("FK_Tenants_Accounts_OwnerId", "Tenants");

            var ownerId = Db.NewId();
            var codeQualityTenantId = Db.NewId();
            migrationBuilder
                .InsertData("Tenants",
                ["Id", "Name", "OwnerId"],
                [codeQualityTenantId, "Code Quality", ownerId]);

            migrationBuilder
                .InsertData("Accounts",
                ["Id", "FirstName", "LastName", "FullName", "Email", "Locale", "TimeZone", "TenantId", "ProfilePictureUrl", "CreatedAt"],
                [ownerId, "Tenant", "Code Quality", "Tenant Code Quality", "codequality@tenant.com", "Uruguay", "-03", codeQualityTenantId, null, DateTime.UtcNow.Date]);

            migrationBuilder
                .AddForeignKey("FK_Tenants_Accounts_OwnerId", "Tenants", "OwnerId", "Accounts");
            #endregion

            #region Apps
            var authProviderWebApiId = Db.NewId();
            migrationBuilder
                .InsertData("Apps",
                ["Id", "Name", "IsDefault", "TenantId"],
                [authProviderWebApiId, "Auth Provider WEB API", true, codeQualityTenantId]);
            #endregion

            #region Permissions
            var jokerId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "AppId", "TenantId"],
                [jokerId, "Comodin", "Comodin", PermissionKey.Joker.ToString(), false, authProviderWebApiId, codeQualityTenantId]);

            var fullAccessId = Db.NewId();
            migrationBuilder
                .InsertData("Permissions",
                ["Id", "Name", "Description", "Key", "IsPublic", "AppId", "TenantId"],
                [fullAccessId, "Full Access", "Full Access", PermissionKey.FullAccess.ToString(), false, authProviderWebApiId, codeQualityTenantId]);
            #endregion

            #region Roles
            var authProviderOwnerId = Db.NewId();
            migrationBuilder
                .InsertData("Roles",
                ["Id", "Name", "Description", "IsPublic", "IsDefault", "TenantId"],
                [authProviderOwnerId, "AuthProvider WEB API Owner", "AuthProvider Owner", false, false, codeQualityTenantId]);
            #endregion

            #region Connect accounts with roles
            migrationBuilder
                .InsertData("AccountsRoles",
                ["Id", "AccountId", "RoleId", "TenantId"],
                [Db.NewId(), ownerId, authProviderOwnerId, codeQualityTenantId]);
            #endregion

            #region Connect roles with permissions
            migrationBuilder
                .InsertData("RolesPermissions",
                ["Id", "RoleId", "PermissionId", "TenantId"],
                [Db.NewId(), authProviderOwnerId, jokerId, codeQualityTenantId]);

            migrationBuilder
               .InsertData("RolesPermissions",
               ["Id", "RoleId", "PermissionId", "TenantId"],
               [Db.NewId(), authProviderOwnerId, fullAccessId, codeQualityTenantId]);
            #endregion

            #region Connect accounts with apps
            migrationBuilder
                .InsertData("AccountsApps",
                ["Id", "AccountId", "AppId", "TenantId"],
                [Db.NewId(), ownerId, authProviderWebApiId, codeQualityTenantId]);
            #endregion

            #region Connect roles with apps
            migrationBuilder
                .InsertData("RolesApps",
                ["Id", "RoleId", "AppId", "TenantId"],
                [Db.NewId(), authProviderOwnerId, authProviderWebApiId, codeQualityTenantId]);
            #endregion

            #region Connect permissions with apps
            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), jokerId, authProviderWebApiId, codeQualityTenantId]);

            migrationBuilder
                .InsertData("PermissionsApps",
                ["Id", "PermissionId", "AppId", "TenantId"],
                [Db.NewId(), fullAccessId, authProviderWebApiId, codeQualityTenantId]);
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
