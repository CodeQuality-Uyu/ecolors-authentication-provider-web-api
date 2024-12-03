using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class MoreRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Permissions over Auth Provider Web Api app", false, true, "Auth Provider Web Api Owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"));
        }
    }
}
