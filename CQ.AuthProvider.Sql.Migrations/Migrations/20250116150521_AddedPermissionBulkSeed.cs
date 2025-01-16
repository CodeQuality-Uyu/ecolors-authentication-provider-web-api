using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedPermissionBulkSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[] { new Guid("e38a9a3a-dea3-46d5-a7a8-d5e9ea882e15"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create several permissions at once", true, "createbulk-permission", "Create permission in bulk", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { new Guid("e38a9a3a-dea3-46d5-a7a8-d5e9ea882e15"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e38a9a3a-dea3-46d5-a7a8-d5e9ea882e15"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e38a9a3a-dea3-46d5-a7a8-d5e9ea882e15"));
        }
    }
}
