using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class MoveToKeyBlobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundCoverId",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundCoverKey",
                table: "Apps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverKey",
                table: "Apps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureKey",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"),
                column: "ProfilePictureKey",
                value: null);

            migrationBuilder.UpdateData(
                table: "Apps",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"),
                columns: new[] { "BackgroundCoverKey", "CoverKey" },
                values: new object[] { null, "auth-web-api-cover.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundCoverKey",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "CoverKey",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "ProfilePictureKey",
                table: "Accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "BackgroundCoverId",
                table: "Apps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CoverId",
                table: "Apps",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"),
                column: "ProfilePictureId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Apps",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"),
                columns: new[] { "BackgroundCoverId", "CoverId" },
                values: new object[] { null, new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
