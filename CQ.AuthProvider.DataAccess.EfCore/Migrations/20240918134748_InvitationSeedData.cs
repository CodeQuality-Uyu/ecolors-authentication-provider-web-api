using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InvitationSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[] { "f3ba5c2342a248d89eee2977456d54cd", "d31184dabbc6435eaec86694650c2679", "Can create invitations", true, "create-invitation", "Can create invitations", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { "c867b020844a4a6fa495b013bc903b3a", "f3ba5c2342a248d89eee2977456d54cd", "5c2260fc58864b75a4cad5c0e7dd57cb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "c867b020844a4a6fa495b013bc903b3a");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "f3ba5c2342a248d89eee2977456d54cd");
        }
    }
}
