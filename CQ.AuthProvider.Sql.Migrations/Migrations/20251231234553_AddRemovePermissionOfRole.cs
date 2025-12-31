using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddRemovePermissionOfRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Apps",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"),
                column: "Name",
                value: "auth-provider-web-api");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can remove permission of role", true, "removepermission-role", "Can remove permission of role", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("780a89b1-9fd3-4cf6-b802-2882ebb3db92"),
                column: "Name",
                value: "Auth Web API owner");

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.UpdateData(
                table: "Apps",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"),
                column: "Name",
                value: "Auth Provider Web Api");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("780a89b1-9fd3-4cf6-b802-2882ebb3db92"),
                column: "Name",
                value: "Auth Provider Web Api Owner");
        }
    }
}
