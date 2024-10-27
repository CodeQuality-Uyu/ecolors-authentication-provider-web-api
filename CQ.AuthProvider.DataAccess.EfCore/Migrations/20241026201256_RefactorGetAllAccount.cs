using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class RefactorGetAllAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "33d7733f42214f6785e10a480c45a007",
                column: "Key",
                value: "getall-account");

            migrationBuilder.UpdateData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "16ef3304b62240b2bd86b4287f14bea3",
                column: "RoleId",
                value: "5c2260fc58864b75a4cad5c0e7dd57cb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "33d7733f42214f6785e10a480c45a007",
                column: "Key",
                value: "getall-accounts");

            migrationBuilder.UpdateData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "16ef3304b62240b2bd86b4287f14bea3",
                column: "RoleId",
                value: "dfa136595e304b98ad7b55d782c6a12c");
        }
    }
}
