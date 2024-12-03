using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class RolePermissionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") },
                    { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") },
                    { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") },
                    { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("06f5a862-9cfd-4c1f-a777-4c4b3adb916b"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("0b2f5e97-42f9-4e56-9ee2-40b033cff9e8"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("27c1378d-39df-4a57-b025-fc96963955a6"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("2eab3c3a-792a-444a-97f3-01db00dffcab"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("32b32564-459f-4e74-8456-83147bd03c9e"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("40bc0960-8c55-488e-a014-f5b52db3d306"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("45104ffc-433c-42bc-a887-18d71d2bc524"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("6323b5da-b78c-4984-a56e-8206775d3e91"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("bcb925af-f4be-4782-978c-c496b044609f"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("216b14a3-337a-45a6-a75d-cae870a73918"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("06f5a862-9cfd-4c1f-a777-4c4b3adb916b"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0b2f5e97-42f9-4e56-9ee2-40b033cff9e8"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("27c1378d-39df-4a57-b025-fc96963955a6"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2eab3c3a-792a-444a-97f3-01db00dffcab"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("32b32564-459f-4e74-8456-83147bd03c9e"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("40bc0960-8c55-488e-a014-f5b52db3d306"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("45104ffc-433c-42bc-a887-18d71d2bc524"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6323b5da-b78c-4984-a56e-8206775d3e91"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("bcb925af-f4be-4782-978c-c496b044609f"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("216b14a3-337a-45a6-a75d-cae870a73918"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc") });
        }
    }
}
