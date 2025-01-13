using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AccountRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsRoles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") });
        }
    }
}
