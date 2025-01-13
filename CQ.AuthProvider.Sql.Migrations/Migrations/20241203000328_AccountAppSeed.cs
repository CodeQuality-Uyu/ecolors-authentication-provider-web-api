using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AccountAppSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountsApps",
                columns: new[] { "AccountId", "AppId" },
                values: new object[] { new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsApps",
                keyColumns: new[] { "AccountId", "AppId" },
                keyValues: new object[] { new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc") });
        }
    }
}
