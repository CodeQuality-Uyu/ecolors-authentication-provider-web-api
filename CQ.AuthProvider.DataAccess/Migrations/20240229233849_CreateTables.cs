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
                values: new object[] { "d4702564-8273-495b-a694-82fcc69da874", new DateTimeOffset(new DateTime(2024, 2, 29, 23, 38, 49, 492, DateTimeKind.Unspecified).AddTicks(469), new TimeSpan(0, 0, 0, 0, 0)), "admin@gmail.com", "Admin Admin" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "IsPublic", "Key", "Name" },
                values: new object[,]
                {
                    { "054240e399f2467b89791ee5678b126b", "Obtener un rol", false, "getbyid-rol", "Obtener un rol" },
                    { "1b19c58b29984320b65b41d57beadd1d", "Actualizar un rol", false, "updatebyid-rol", "Actualizar un rol" },
                    { "1de1006bcc3644769f82d4e2431e522c", "Obtener roles publicos", false, "getpublic-rol", "Obtener roles publicos" },
                    { "45779b456d0f478da758125001503210", "Obtener todos los permisos", false, "getall-permission", "Obtener permisos" },
                    { "5901e6ddaa8e4846932feeff2b7eb43a", "Obtener todos los roles", false, "getall-permission", "Obtener roles" },
                    { "5f2e5b22569b4f6792b13ae81ed8c958", "Crear permiso", false, "create-permission", "Crear permiso" },
                    { "69c149db690b4825a3e55d49256cd9ee", "Actualizar un permiso", false, "updatebyid-permission", "Actualizar un permiso" },
                    { "812c61a4cfa74f3d867d3e605186c8ca", "Obtener un permiso", false, "getbyid-permission", "Obtener un permiso" },
                    { "cd94dfb75151470f905f4e24604c1871", "Obtener permisos publicos", false, "getpublic-permission", "Obtener permisos publicos" },
                    { "fd325e386f7c4652bbab42712a65c2c8", "Crear rol", false, "create-rol", "Crear rol" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "IsPublic", "Key", "Name" },
                values: new object[] { "ad34233fc5694b019b7c5cc5e5c959b2", "Admin", false, "admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { "d4702564-8273-495b-a694-82fcc69da874", "ad34233fc5694b019b7c5cc5e5c959b2" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "054240e399f2467b89791ee5678b126b", "ad34233fc5694b019b7c5cc5e5c959b2" },
                    { "1b19c58b29984320b65b41d57beadd1d", "ad34233fc5694b019b7c5cc5e5c959b2" },
                    { "1de1006bcc3644769f82d4e2431e522c", "ad34233fc5694b019b7c5cc5e5c959b2" },
                    { "45779b456d0f478da758125001503210", "ad34233fc5694b019b7c5cc5e5c959b2" },
                    { "5901e6ddaa8e4846932feeff2b7eb43a", "ad34233fc5694b019b7c5cc5e5c959b2" },
                    { "5f2e5b22569b4f6792b13ae81ed8c958", "ad34233fc5694b019b7c5cc5e5c959b2" },
                    { "69c149db690b4825a3e55d49256cd9ee", "ad34233fc5694b019b7c5cc5e5c959b2" },
                    { "812c61a4cfa74f3d867d3e605186c8ca", "ad34233fc5694b019b7c5cc5e5c959b2" },
                    { "cd94dfb75151470f905f4e24604c1871", "ad34233fc5694b019b7c5cc5e5c959b2" },
                    { "fd325e386f7c4652bbab42712a65c2c8", "ad34233fc5694b019b7c5cc5e5c959b2" }
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
