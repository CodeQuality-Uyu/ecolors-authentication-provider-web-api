using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateTimeToOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Accounts",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"),
                column: "CreatedAt",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
