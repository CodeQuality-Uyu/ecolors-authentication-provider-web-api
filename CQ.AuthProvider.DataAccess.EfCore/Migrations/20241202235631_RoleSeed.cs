using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[,]
                {
                    { new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Should be deleted once deployed", false, false, "Seed", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Tenant Owner", false, true, "Tenant Owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e"));
        }
    }
}
