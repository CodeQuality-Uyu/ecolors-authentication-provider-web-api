using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.Postgres.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedFatherAppToApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FatherAppId",
                table: "Apps",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Apps",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"),
                column: "FatherAppId",
                value: null);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[,]
                {
                    { new Guid("43da8440-39be-46cc-b8fe-da34961d2486"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read clients of tenant", true, "getall-client", "Can read clients", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("87013d07-c8ba-48f1-bb8c-510b7836fe1f"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create clients of tenant", true, "create-client", "Can create clients", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "App owner", false, true, "App owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c"),
                columns: new[] { "CoverLogoId", "MiniLogoId" },
                values: new object[] { new Guid("d7cb8b70-f3e9-4ffa-a963-c72942a7f65b"), new Guid("0f491b27-2a93-479a-b674-5c49db77f05c") });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("2eab3c3a-792a-444a-97f3-01db00dffcab"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") },
                    { new Guid("43da8440-39be-46cc-b8fe-da34961d2486"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apps_FatherAppId",
                table: "Apps",
                column: "FatherAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_Apps_FatherAppId",
                table: "Apps",
                column: "FatherAppId",
                principalTable: "Apps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_Apps_FatherAppId",
                table: "Apps");

            migrationBuilder.DropIndex(
                name: "IX_Apps_FatherAppId",
                table: "Apps");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("87013d07-c8ba-48f1-bb8c-510b7836fe1f"));

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2eab3c3a-792a-444a-97f3-01db00dffcab"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("43da8440-39be-46cc-b8fe-da34961d2486"), new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("43da8440-39be-46cc-b8fe-da34961d2486"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4579a206-b6c7-4d58-9d36-c3e0923041b5"));

            migrationBuilder.DropColumn(
                name: "FatherAppId",
                table: "Apps");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c"),
                columns: new[] { "CoverLogoId", "MiniLogoId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
