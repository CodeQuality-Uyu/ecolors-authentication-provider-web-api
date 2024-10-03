using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class SeedTenantTransferPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[] { "cd4566d8e48b42ce9854722fdaae2fbc", "d31184dabbc6435eaec86694650c2679", "Transfer ownership of tenant", true, "transfertenant-me", "Transfer ownership of tenant", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "7ed16be85568479585b49d8ad1a96d2e", "cd4566d8e48b42ce9854722fdaae2fbc", "dfa136595e304b98ad7b55d782c6a12c" },
                    { "edebfdc59b864c3e83b5e353aebfcc26", "cd4566d8e48b42ce9854722fdaae2fbc", "5c2260fc58864b75a4cad5c0e7dd57cb" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "7ed16be85568479585b49d8ad1a96d2e");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "edebfdc59b864c3e83b5e353aebfcc26");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "cd4566d8e48b42ce9854722fdaae2fbc");
        }
    }
}
