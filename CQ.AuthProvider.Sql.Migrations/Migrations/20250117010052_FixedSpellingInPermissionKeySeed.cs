using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class FixedSpellingInPermissionKeySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c0a55e4b-b24d-42a4-90e4-f828e2b8e098"),
                column: "Key",
                value: "updateroles-account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c0a55e4b-b24d-42a4-90e4-f828e2b8e098"),
                column: "Key",
                value: "udpdateroles-account");
        }
    }
}
