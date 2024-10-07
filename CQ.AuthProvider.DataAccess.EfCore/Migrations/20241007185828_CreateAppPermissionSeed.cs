using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class CreateAppPermissionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "9203d8a99d4e4a3b9b47f7db0e81353e",
                column: "Key",
                value: "create-tenant");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[] { "7e9af6ea241342c5bb97c634a36c2de2", "d31184dabbc6435eaec86694650c2679", "Can create app", true, "create-app", "Can create app", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { "f368580391cc459c964ce099cebb9b02", "7e9af6ea241342c5bb97c634a36c2de2", "5c2260fc58864b75a4cad5c0e7dd57cb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "f368580391cc459c964ce099cebb9b02");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "7e9af6ea241342c5bb97c634a36c2de2");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "9203d8a99d4e4a3b9b47f7db0e81353e",
                column: "Key",
                value: "create-tenants");
        }
    }
}
