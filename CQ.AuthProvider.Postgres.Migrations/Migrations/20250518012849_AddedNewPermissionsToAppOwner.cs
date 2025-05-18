using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.Postgres.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewPermissionsToAppOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") },
                    { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") });
        }
    }
}
