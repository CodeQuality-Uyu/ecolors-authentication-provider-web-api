using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnusedPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "8d01aeac30dd45599da743bcc3f3ee0d");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "922a41f8597742178605a5ea7c75be32");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "a85e6d40858e4451b8a103bd903b6269");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "e568c9eb81b24a5cae922c2a9a2ebc41");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "fe7a5b81f9284af1857621c234ebc615");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "05276f2a25dc4db5b37b0948e05c35ab");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "1554a06426024ee88baabad7a71d65cf");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "1ce9908dba38490cbc65389bfeece21e");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "d1d34f71201f4b3e8f1c232aef35c40a");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "e0132221c91f44ada257a38d951407d6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[,]
                {
                    { "05276f2a25dc4db5b37b0948e05c35ab", "d31184dabbc6435eaec86694650c2679", "Can filter permissions by role", false, "can-read-permissions-of-role", "Can filter permissions by role", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "1554a06426024ee88baabad7a71d65cf", "d31184dabbc6435eaec86694650c2679", "Can read roles of tenant", true, "can-read-roles-of-tenant", "Can read roles of tenant", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "1ce9908dba38490cbc65389bfeece21e", "d31184dabbc6435eaec86694650c2679", "Can read private roles", false, "can-read-private-roles", "Can read private roles", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "d1d34f71201f4b3e8f1c232aef35c40a", "d31184dabbc6435eaec86694650c2679", "Can read permissions of tenant", false, "can-read-tenants-permissions", "Can read permissions of tenant", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "e0132221c91f44ada257a38d951407d6", "d31184dabbc6435eaec86694650c2679", "Can read private permissions", false, "can-read-private-permissions", "Can read private permissions", "b22fcf202bd84a97936ccf2949e00da4" }
                });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "8d01aeac30dd45599da743bcc3f3ee0d", "e0132221c91f44ada257a38d951407d6", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "922a41f8597742178605a5ea7c75be32", "1ce9908dba38490cbc65389bfeece21e", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "a85e6d40858e4451b8a103bd903b6269", "05276f2a25dc4db5b37b0948e05c35ab", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "e568c9eb81b24a5cae922c2a9a2ebc41", "d1d34f71201f4b3e8f1c232aef35c40a", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "fe7a5b81f9284af1857621c234ebc615", "1554a06426024ee88baabad7a71d65cf", "5c2260fc58864b75a4cad5c0e7dd57cb" }
                });
        }
    }
}
