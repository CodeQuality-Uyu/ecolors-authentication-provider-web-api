using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolesPermissions_TenantId",
                table: "RolesPermissions");

            migrationBuilder.DropIndex(
                name: "IX_AccountsRoles_TenantId",
                table: "AccountsRoles");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "RolesPermissions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AccountsRoles");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[,]
                {
                    { "05276f2a25dc4db5b37b0948e05c35ab", "d31184dabbc6435eaec86694650c2679", "Can read roles of tenant", false, "can-read-roles-of-tenant", "Can read roles of tenant", null },
                    { "1ce9908dba38490cbc65389bfeece21e", "d31184dabbc6435eaec86694650c2679", "Can read private roles", false, "can-read-private-roles", "Can read private roles", null },
                    { "80ca0e41ea5046519f351a99b03b294e", "d31184dabbc6435eaec86694650c2679", "Can read invitations of tenant", false, "can-read-invitations-of-tenant", "Can read invitations of tenant", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "aca002cfbf3a47899ff4c16e6be2c029", "d31184dabbc6435eaec86694650c2679", "Can read roles", true, "getall-role", "Can read roles", null },
                    { "d40ad347c7f943e399069eef409b4fa6", "d31184dabbc6435eaec86694650c2679", "Can read permissions", true, "getall-permission", "Can read permissions", null },
                    { "d56a38db0db2439f8ee15a142b22b33b", "d31184dabbc6435eaec86694650c2679", "Can read permissions of tenant", false, "can-read-permissions-of-tenant", "Can read permissions of tenant", null },
                    { "e0132221c91f44ada257a38d951407d6", "d31184dabbc6435eaec86694650c2679", "Can read private permissions", false, "can-read-private-permissions", "Can read private permissions", null }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4c00f792d8ed43768846711094902d8c",
                column: "IsDefault",
                value: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { "5c2260fc58864b75a4cad5c0e7dd57cb", "d31184dabbc6435eaec86694650c2679", "Tenant Owner", false, true, "Tenant Owner", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { "080873f63bff4e9a9687ac70658b710b", "e2d42874c56e46319b21eeb817f3b988", "5c2260fc58864b75a4cad5c0e7dd57cb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "05276f2a25dc4db5b37b0948e05c35ab");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "1ce9908dba38490cbc65389bfeece21e");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "80ca0e41ea5046519f351a99b03b294e");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "aca002cfbf3a47899ff4c16e6be2c029");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "d40ad347c7f943e399069eef409b4fa6");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "d56a38db0db2439f8ee15a142b22b33b");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "e0132221c91f44ada257a38d951407d6");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "080873f63bff4e9a9687ac70658b710b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5c2260fc58864b75a4cad5c0e7dd57cb");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "RolesPermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "AccountsRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AccountsRoles",
                keyColumn: "Id",
                keyValue: "9dc669b28b0f4f3fb8a832961a76a6c9",
                column: "TenantId",
                value: "b22fcf202bd84a97936ccf2949e00da4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4c00f792d8ed43768846711094902d8c",
                column: "IsDefault",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "4909564462b040289d0dc0758cf8942e",
                column: "TenantId",
                value: "b22fcf202bd84a97936ccf2949e00da4");

            migrationBuilder.UpdateData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "64ec1b6bbd3d4c49b609c0f58359e7ac",
                column: "TenantId",
                value: "b22fcf202bd84a97936ccf2949e00da4");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_TenantId",
                table: "RolesPermissions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsRoles_TenantId",
                table: "AccountsRoles",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsRoles_Tenants_TenantId",
                table: "AccountsRoles",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolesPermissions_Tenants_TenantId",
                table: "RolesPermissions",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
