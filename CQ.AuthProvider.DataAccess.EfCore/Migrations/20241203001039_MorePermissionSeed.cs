using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class MorePermissionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[] { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create accounts", true, "createcredentialsfor-account", "Can create accounts", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"));
        }
    }
}
