using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class RefactorLogoAndBackground : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Apps");

            migrationBuilder.RenameColumn(
                name: "CoverKey",
                table: "Apps",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "BackgroundCoverKey",
                table: "Apps",
                newName: "Background");

            migrationBuilder.UpdateData(
                table: "Apps",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"),
                column: "Logo",
                value: "{\"ColorKey\":\"auth-web-api-logo-color.png\",\"LightKey\":\"auth-web-api-logo-light.png\",\"DarkKey\":\"auth-web-api-logo-dark.png\"}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "Apps",
                newName: "CoverKey");

            migrationBuilder.RenameColumn(
                name: "Background",
                table: "Apps",
                newName: "BackgroundCoverKey");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Apps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Apps",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"),
                columns: new[] { "BackgroundColor", "CoverKey" },
                values: new object[] { null, "auth-web-api-cover.png" });
        }
    }
}
