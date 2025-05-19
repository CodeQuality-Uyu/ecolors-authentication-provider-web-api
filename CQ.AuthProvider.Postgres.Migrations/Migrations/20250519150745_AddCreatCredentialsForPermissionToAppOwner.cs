using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.Postgres.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatCredentialsForPermissionToAppOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") });
        }
    }
}
