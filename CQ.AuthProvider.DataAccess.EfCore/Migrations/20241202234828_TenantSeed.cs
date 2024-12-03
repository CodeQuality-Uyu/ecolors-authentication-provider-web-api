using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class TenantSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "OwnerId" },
                values: new object[] { new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c"), "Seed Tenant", new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c"));
        }
    }
}
