using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.Postgres.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOwnerOfTenantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tenants");

            migrationBuilder.AlterColumn<Guid>(
                name: "MiniLogoId",
                table: "Tenants",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoverLogoId",
                table: "Tenants",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("216b14a3-337a-45a6-a75d-cae870a73918"),
                column: "Key",
                value: "getall-tenant");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c"),
                columns: new[] { "CoverLogoId", "MiniLogoId" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "MiniLogoId",
                table: "Tenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CoverLogoId",
                table: "Tenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Tenants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("216b14a3-337a-45a6-a75d-cae870a73918"),
                column: "Key",
                value: "getall-tenants");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c"),
                columns: new[] { "CoverLogoId", "MiniLogoId", "OwnerId" },
                values: new object[] { new Guid("d7cb8b70-f3e9-4ffa-a963-c72942a7f65b"), new Guid("0f491b27-2a93-479a-b674-5c49db77f05c"), new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d") });
        }
    }
}
