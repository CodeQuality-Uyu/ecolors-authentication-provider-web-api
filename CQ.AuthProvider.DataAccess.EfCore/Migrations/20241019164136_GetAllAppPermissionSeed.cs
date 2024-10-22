using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class GetAllAppPermissionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[] { "843aa6fb505b4f919930aeeea10511ee", "d31184dabbc6435eaec86694650c2679", "Can read apps of tenant", true, "getall-app", "Can read apps", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { "116fcf12e6aa43fa837dce2199ce195c", "843aa6fb505b4f919930aeeea10511ee", "5c2260fc58864b75a4cad5c0e7dd57cb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "116fcf12e6aa43fa837dce2199ce195c");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "843aa6fb505b4f919930aeeea10511ee");
        }
    }
}
