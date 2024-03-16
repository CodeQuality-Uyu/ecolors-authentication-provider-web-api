using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResetPasswordApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPasswordApplications_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsRoles",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsRoles", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountsRoles_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountsRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Email", "Name" },
                values: new object[] { "d4702564-8273-495b-a694-82fcc69da874", new DateTimeOffset(new DateTime(2024, 3, 2, 21, 27, 46, 256, DateTimeKind.Unspecified).AddTicks(6917), new TimeSpan(0, 0, 0, 0, 0)), "admin@gmail.com", "Admin Admin" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "IsPublic", "Key", "Name" },
                values: new object[,]
                {
                    { "0682cd66199645e5a1fb5ba483cbf9c8", "Actualizar un permiso", false, "updatebyid-permission", "Actualizar un permiso" },
                    { "13ce67315ae5464c889a25379c6b32a5", "Obtener un permiso", false, "getbyid-permission", "Obtener un permiso" },
                    { "369a991cec3f40f489c821774d3ede06", "Obtener permisos privados", false, "getallprivate-permission", "Obtener permisos privados" },
                    { "46536c750bda4544873621b341667faa", "Obtener todos los roles", false, "getall-role", "Obtener roles" },
                    { "6a41bc423a494a63a80f5da9dd0ae277", "Crear permiso", false, "create-permission", "Crear permiso" },
                    { "7815af8db7a1454f896c3adfd26f341c", "Crear rol", false, "create-role", "Crear rol" },
                    { "78d5bee419524cfeb3596078878427d0", "Obtener permisos de un rol", false, "getallbyroleid-permission", "Obtener permisos de un rol" },
                    { "7e702bcc9a704e53b1842dccb90271fb", "Obtener todos los permisos", false, "getall-permission", "Obtener permisos" },
                    { "82f67574fc7c45baae4bee48445f6c7b", "Obtener roles privados", false, "getallprivate-role", "Obtener roles privados" },
                    { "e894d54c71e24fff9d14f9dc3e35663d", "Obtener un rol", false, "getbyid-role", "Obtener un rol" },
                    { "ea6d3f0d41ea482393d00d23cfce9f24", "Actualizar un rol", false, "addpermission-role", "Actualizar un rol" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "IsPublic", "Key", "Name" },
                values: new object[] { "ebe421fd52724468bb1b870cf30eec1b", "Admin", false, "admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { "d4702564-8273-495b-a694-82fcc69da874", "ebe421fd52724468bb1b870cf30eec1b" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "0682cd66199645e5a1fb5ba483cbf9c8", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "13ce67315ae5464c889a25379c6b32a5", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "369a991cec3f40f489c821774d3ede06", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "46536c750bda4544873621b341667faa", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "6a41bc423a494a63a80f5da9dd0ae277", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "7815af8db7a1454f896c3adfd26f341c", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "78d5bee419524cfeb3596078878427d0", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "7e702bcc9a704e53b1842dccb90271fb", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "82f67574fc7c45baae4bee48445f6c7b", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "e894d54c71e24fff9d14f9dc3e35663d", "ebe421fd52724468bb1b870cf30eec1b" },
                    { "ea6d3f0d41ea482393d00d23cfce9f24", "ebe421fd52724468bb1b870cf30eec1b" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsRoles_RoleId",
                table: "AccountsRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordApplications_AccountId",
                table: "ResetPasswordApplications",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_RoleId",
                table: "RolesPermissions",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsRoles");

            migrationBuilder.DropTable(
                name: "ResetPasswordApplications");

            migrationBuilder.DropTable(
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
