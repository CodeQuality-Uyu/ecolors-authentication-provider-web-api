using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class RefactorSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[,]
                {
                    { "33d7733f42214f6785e10a480c45a007", "d31184dabbc6435eaec86694650c2679", "Can read all accounts", true, "getall-accounts", "Can read all accounts", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "7d21bd25e0b74951b06772ca348e81fa", "d31184dabbc6435eaec86694650c2679", "Can update tenant name", true, "patchname-tenants", "Can update tenant name", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "91cc2fb3a90e4f4aa01c02a363ae44c3", "d31184dabbc6435eaec86694650c2679", "Can update tenant owner", true, "patchowner-tenants", "Can update tenant owner", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "9203d8a99d4e4a3b9b47f7db0e81353e", "d31184dabbc6435eaec86694650c2679", "Can create tenant", true, "create-tenants", "Can create tenant", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "9b079f7461554950bbd981f929568322", "d31184dabbc6435eaec86694650c2679", "Can read all tenants", true, "getall-tenants", "Can read all tenants", "b22fcf202bd84a97936ccf2949e00da4" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { "dfa136595e304b98ad7b55d782c6a12c", "d31184dabbc6435eaec86694650c2679", "Owner of Auth Web Api App", false, true, "Auth Web Api Owner", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "Id", "AccountId", "RoleId" },
                values: new object[] { "735eeea5c936492794514b5cc3ecd217", "5a0d9e179991499e80db0a15fda4df79", "dfa136595e304b98ad7b55d782c6a12c" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "16ef3304b62240b2bd86b4287f14bea3", "33d7733f42214f6785e10a480c45a007", "dfa136595e304b98ad7b55d782c6a12c" },
                    { "20ba3bbf9e87433199a49bc01c928014", "91cc2fb3a90e4f4aa01c02a363ae44c3", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "60307119a6f8403faaf53606eceefedc", "7d21bd25e0b74951b06772ca348e81fa", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "89c5ad347a8f41c0864a4a37f7be5224", "9b079f7461554950bbd981f929568322", "dfa136595e304b98ad7b55d782c6a12c" },
                    { "8c52753c02324daeb56fb4557c2eaf46", "9203d8a99d4e4a3b9b47f7db0e81353e", "5c2260fc58864b75a4cad5c0e7dd57cb" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsRoles",
                keyColumn: "Id",
                keyValue: "735eeea5c936492794514b5cc3ecd217");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "16ef3304b62240b2bd86b4287f14bea3");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "20ba3bbf9e87433199a49bc01c928014");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "60307119a6f8403faaf53606eceefedc");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "89c5ad347a8f41c0864a4a37f7be5224");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "8c52753c02324daeb56fb4557c2eaf46");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "33d7733f42214f6785e10a480c45a007");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "7d21bd25e0b74951b06772ca348e81fa");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "91cc2fb3a90e4f4aa01c02a363ae44c3");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "9203d8a99d4e4a3b9b47f7db0e81353e");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "9b079f7461554950bbd981f929568322");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dfa136595e304b98ad7b55d782c6a12c");
        }
    }
}
