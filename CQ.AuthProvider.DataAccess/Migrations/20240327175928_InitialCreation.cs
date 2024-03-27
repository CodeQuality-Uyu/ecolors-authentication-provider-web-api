using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
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
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
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
                name: "ClientSystems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrivateKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSystems_Roles_RoleId",
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
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "FullName", "LastName" },
                values: new object[] { "d47025648273495ba69482fcc69da874", new DateTimeOffset(new DateTime(2024, 3, 27, 17, 59, 27, 894, DateTimeKind.Unspecified).AddTicks(9841), new TimeSpan(0, 0, 0, 0, 0)), "admin@gmail.com", "Admin", "Admin Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "IsPublic", "Key", "Name" },
                values: new object[,]
                {
                    { "0031a0bc0aac4e2a9cd94ed9796bbee8", "Crear muchos roles", false, "createbulk-role", "Crear muchos roles" },
                    { "073b1c6e9ddf4ebd9528aaa64d938916", "Actualizar un permiso", false, "updatebyid-permission", "Actualizar un permiso" },
                    { "0a0027c493804ee39499684aabc36c6d", "Obtener usuario por id de sistema cliente", false, "getbyidfromauthprovider-user", "Obtener usuario por id de sistema cliente" },
                    { "105bb9d29d0f4928b6dceb7d9da744ba", "Crear permiso", false, "create-permission", "Crear permiso" },
                    { "1e5792159aeb465fba1368f7ff835e2c", "Crear muchos permisos", false, "createbulk-permission", "Crear muchos permisos" },
                    { "243e9036a6b542478e068e56a30e11d1", "Obtener todos los roles", false, "getall-role", "Obtener roles" },
                    { "5f83e7a5640e4eb4aa7f4c3e0b6985ac", "Obtener permisos de un rol", false, "getallbyroleid-permission", "Obtener permisos de un rol" },
                    { "6ca46b22fcd74070a55d883682991637", "Obtener un permiso", false, "getbyid-permission", "Obtener un permiso" },
                    { "7d97d5a3a3624a94a9dcde6aa2a55339", "Obtener todos los permisos", false, "getall-permission", "Obtener permisos" },
                    { "85c4f11e41c64468a9eb4b657d7f992d", "Joker", false, "*", "Joker" },
                    { "9af1865df2fc487b8456c39625988ced", "Validar token", false, "validatetoken-session", "Validar token" },
                    { "b3d1e8d0e72b4bef864417feec140d7c", "Crear cuenta para un usuario", false, "createcredentialsfor-account", "Crear cuenta para un usuario" },
                    { "b67bdf587d50422f907d065d9e709bf9", "Obtener roles privados", false, "getallprivate-role", "Obtener roles privados" },
                    { "d750f3ec5a824cd8995bd981facab2bc", "Obtener la cuenta por token de sistema cliente", false, "getbytoken-account", "Obtener la cuenta por token de sistema cliente" },
                    { "da51cccd7517426ca78d8c6bbec9026c", "Crear rol", false, "create-role", "Crear rol" },
                    { "ed6dd49edba74f5da4f898ec6c860481", "Obtener permisos privados", false, "getallprivate-permission", "Obtener permisos privados" },
                    { "ef577b64f9c741f6b68a2732db49f2e5", "Crear usuario en sistema cliente", false, "createfromauthprovider-user", "Crear usuario en sistema cliente" },
                    { "f0b3321ff1c6426092c398ff3e4110f2", "Crear client system", false, "create-clientsystem", "Crear client system" },
                    { "f16e3d89c187463791da4625fdb14589", "Obtener un rol", false, "getbyid-role", "Obtener un rol" },
                    { "f1ae8a44eb954f5083d4f48dbd321525", "Actualizar un rol", false, "addpermission-role", "Actualizar un rol" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "IsDefault", "IsPublic", "Key", "Name" },
                values: new object[,]
                {
                    { "6565ebacc3a94007873967d083e9def0", "Client System", false, false, "clientSystem", "Client System" },
                    { "71bcf11229094af4bd29239f6e850688", "Admin", false, false, "admin", "Admin" },
                    { "bcc1589004f54f0e91e67295a46a9140", "Auth provider", false, false, "authProviderClientSystem", "Auth provider" }
                });

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { "d47025648273495ba69482fcc69da874", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.InsertData(
                table: "ClientSystems",
                columns: new[] { "Id", "CreatedOn", "Name", "PrivateKey", "RoleId" },
                values: new object[] { "c7af32e7bbc34e26a72924c0d3ad9aad", new DateTimeOffset(new DateTime(2024, 3, 27, 17, 59, 27, 894, DateTimeKind.Unspecified).AddTicks(9851), new TimeSpan(0, 0, 0, 0, 0)), "Authentication Server Provider", "250f9196a3df4163b5a249f30a3a1382", "bcc1589004f54f0e91e67295a46a9140" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "0031a0bc0aac4e2a9cd94ed9796bbee8", "6565ebacc3a94007873967d083e9def0" },
                    { "073b1c6e9ddf4ebd9528aaa64d938916", "71bcf11229094af4bd29239f6e850688" },
                    { "0a0027c493804ee39499684aabc36c6d", "bcc1589004f54f0e91e67295a46a9140" },
                    { "105bb9d29d0f4928b6dceb7d9da744ba", "71bcf11229094af4bd29239f6e850688" },
                    { "1e5792159aeb465fba1368f7ff835e2c", "6565ebacc3a94007873967d083e9def0" },
                    { "243e9036a6b542478e068e56a30e11d1", "71bcf11229094af4bd29239f6e850688" },
                    { "5f83e7a5640e4eb4aa7f4c3e0b6985ac", "71bcf11229094af4bd29239f6e850688" },
                    { "6ca46b22fcd74070a55d883682991637", "71bcf11229094af4bd29239f6e850688" },
                    { "7d97d5a3a3624a94a9dcde6aa2a55339", "71bcf11229094af4bd29239f6e850688" },
                    { "9af1865df2fc487b8456c39625988ced", "6565ebacc3a94007873967d083e9def0" },
                    { "b3d1e8d0e72b4bef864417feec140d7c", "71bcf11229094af4bd29239f6e850688" },
                    { "b67bdf587d50422f907d065d9e709bf9", "71bcf11229094af4bd29239f6e850688" },
                    { "d750f3ec5a824cd8995bd981facab2bc", "6565ebacc3a94007873967d083e9def0" },
                    { "da51cccd7517426ca78d8c6bbec9026c", "71bcf11229094af4bd29239f6e850688" },
                    { "ed6dd49edba74f5da4f898ec6c860481", "71bcf11229094af4bd29239f6e850688" },
                    { "ef577b64f9c741f6b68a2732db49f2e5", "bcc1589004f54f0e91e67295a46a9140" },
                    { "f0b3321ff1c6426092c398ff3e4110f2", "71bcf11229094af4bd29239f6e850688" },
                    { "f16e3d89c187463791da4625fdb14589", "71bcf11229094af4bd29239f6e850688" },
                    { "f1ae8a44eb954f5083d4f48dbd321525", "71bcf11229094af4bd29239f6e850688" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsRoles_RoleId",
                table: "AccountsRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSystems_RoleId",
                table: "ClientSystems",
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
                name: "ClientSystems");

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
