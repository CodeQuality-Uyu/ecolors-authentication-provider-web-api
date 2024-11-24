using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoleAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsRoles",
                keyColumn: "Id",
                keyValue: "735eeea5c936492794514b5cc3ecd217");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { "0415b39e83cd4fbdb33c5004a0b65294", "d31184dabbc6435eaec86694650c2679", "Should be deleted once deployed", false, false, "Seed", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder.UpdateData(
                table: "AccountsRoles",
                keyColumn: "Id",
                keyValue: "1f191c90510d456d84bda9e17fe24f50",
                column: "RoleId",
                value: "0415b39e83cd4fbdb33c5004a0b65294");

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "2ea1bb330e3e489cbf3402daacef9905", "aca002cfbf3a47899ff4c16e6be2c029", "0415b39e83cd4fbdb33c5004a0b65294" },
                    { "ea84e710483e42eea573260151916d36", "f3ba5c2342a248d89eee2977456d54cd", "0415b39e83cd4fbdb33c5004a0b65294" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "2ea1bb330e3e489cbf3402daacef9905");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "ea84e710483e42eea573260151916d36");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0415b39e83cd4fbdb33c5004a0b65294");

            migrationBuilder.UpdateData(
                table: "AccountsRoles",
                keyColumn: "Id",
                keyValue: "1f191c90510d456d84bda9e17fe24f50",
                column: "RoleId",
                value: "5c2260fc58864b75a4cad5c0e7dd57cb");

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "Id", "AccountId", "RoleId" },
                values: new object[] { "735eeea5c936492794514b5cc3ecd217", "5a0d9e179991499e80db0a15fda4df79", "dfa136595e304b98ad7b55d782c6a12c" });
        }
    }
}
