using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AppSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Apps",
                columns: new[] { "Id", "IsDefault", "Name", "TenantId" },
                values: new object[] { new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), true, "Auth Provider Web Api", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Apps",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"));
        }
    }
}
