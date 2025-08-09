using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.IdentityProvider.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class HashingSeedPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Identities",
                keyColumn: "Id",
                keyValue: new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEPsvS9UPGBepUkrx3vhkeyoOBVrQFUURtbldx6xuqpW79GVKXbChBf37/GRGw3N+0w==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Identities",
                keyColumn: "Id",
                keyValue: new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"),
                column: "Password",
                value: "!12345678");
        }
    }
}
