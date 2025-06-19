using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedClientRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[] { new Guid("cfd3f238-a446-4f4f-81f0-f770974f0cc3"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can update colors of app in tenant", true, "updatecolors-app", "Can update colors of app", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Owner of an app that is client of other App", false, true, "Client owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d") },
                    { new Guid("cfd3f238-a446-4f4f-81f0-f770974f0cc3"), new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d") },
                    { new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"), new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d") },
                    { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d") },
                    { new Guid("cfd3f238-a446-4f4f-81f0-f770974f0cc3"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cfd3f238-a446-4f4f-81f0-f770974f0cc3"), new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"), new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cfd3f238-a446-4f4f-81f0-f770974f0cc3"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cfd3f238-a446-4f4f-81f0-f770974f0cc3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("01e55142-6b8c-4e7e-9d71-1e459d07796d"));
        }
    }
}
