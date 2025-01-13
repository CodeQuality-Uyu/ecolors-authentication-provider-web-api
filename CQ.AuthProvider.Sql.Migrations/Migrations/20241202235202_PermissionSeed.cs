using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class PermissionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[,]
                {
                    { new Guid("06f5a862-9cfd-4c1f-a777-4c4b3adb916b"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can update tenant name", true, "updatetenantname-me", "Can update tenant name", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("0b2f5e97-42f9-4e56-9ee2-40b033cff9e8"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create invitations", true, "create-invitation", "Can create invitations", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("216b14a3-337a-45a6-a75d-cae870a73918"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read all tenants", true, "getall-tenants", "Can read all tenants", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("27c1378d-39df-4a57-b025-fc96963955a6"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read all accounts", true, "getall-account", "Can read all accounts", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("2eab3c3a-792a-444a-97f3-01db00dffcab"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create app", true, "create-app", "Can create app", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("32b32564-459f-4e74-8456-83147bd03c9e"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create permissions", true, "create-permission", "Create permission", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("40bc0960-8c55-488e-a014-f5b52db3d306"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read invitations of tenant", true, "getall-invitation", "Can read invitations of tenant", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("45104ffc-433c-42bc-a887-18d71d2bc524"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create tenant", true, "create-tenant", "Can create tenant", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("6323b5da-b78c-4984-a56e-8206775d3e91"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read apps of tenant", true, "getall-app", "Can read apps", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can update tenant owner", true, "transfertenant-me", "Can update tenant owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("bcb925af-f4be-4782-978c-c496b044609f"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read permissions", true, "getall-permission", "Can read permissions", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create roles", true, "create-role", "Can create role", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read roles", true, "getall-role", "Can read roles", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("06f5a862-9cfd-4c1f-a777-4c4b3adb916b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0b2f5e97-42f9-4e56-9ee2-40b033cff9e8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("216b14a3-337a-45a6-a75d-cae870a73918"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("27c1378d-39df-4a57-b025-fc96963955a6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2eab3c3a-792a-444a-97f3-01db00dffcab"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("32b32564-459f-4e74-8456-83147bd03c9e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("40bc0960-8c55-488e-a014-f5b52db3d306"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("45104ffc-433c-42bc-a887-18d71d2bc524"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6323b5da-b78c-4984-a56e-8206775d3e91"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bcb925af-f4be-4782-978c-c496b044609f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"));
        }
    }
}
