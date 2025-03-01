using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345"),
                column: "Description",
                value: "Should be deleted once used");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345"),
                column: "Description",
                value: "Should be deleted once deployed");

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") });
        }
    }
}
