using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AccountSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "FullName", "LastName", "Locale", "ProfilePictureId", "TenantId", "TimeZone" },
                values: new object[] { new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "seed@cq.com", "Seed", "Seed Seed", "Seed", "Uruguay", null, new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c"), "-3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"));
        }
    }
}
