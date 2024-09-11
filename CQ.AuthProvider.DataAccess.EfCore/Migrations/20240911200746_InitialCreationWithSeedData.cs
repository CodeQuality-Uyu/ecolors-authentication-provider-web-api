using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreationWithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apps_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResetPasswords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPasswords_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsApps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsApps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsApps_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountsApps_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    AppId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    AppId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountsRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsRoles", x => x.Id);
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "OwnerId" },
                values: new object[] { "b22fcf202bd84a97936ccf2949e00da4", "Seed Tenant", "5a0d9e179991499e80db0a15fda4df79" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "FullName", "LastName", "Locale", "ProfilePictureId", "TenantId", "TimeZone" },
                values: new object[] { "5a0d9e179991499e80db0a15fda4df79", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "seed@cq.com", "Seed", "Seed Seed", "Seed", "Uruguay", null, "b22fcf202bd84a97936ccf2949e00da4", "-3" });

            migrationBuilder.InsertData(
                table: "Apps",
                columns: new[] { "Id", "IsDefault", "Name", "TenantId" },
                values: new object[] { "d31184dabbc6435eaec86694650c2679", true, "Auth Provider WEB API", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder.InsertData(
                table: "AccountsApps",
                columns: new[] { "Id", "AccountId", "AppId" },
                values: new object[] { "ef03980ea2a54e4bba92e022fbd33d9b", "5a0d9e179991499e80db0a15fda4df79", "d31184dabbc6435eaec86694650c2679" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[,]
                {
                    { "05276f2a25dc4db5b37b0948e05c35ab", "d31184dabbc6435eaec86694650c2679", "Can filter permissions by role", false, "can-read-permissions-of-role", "Can filter permissions by role", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "1554a06426024ee88baabad7a71d65cf", "d31184dabbc6435eaec86694650c2679", "Can read roles of tenant", true, "can-read-roles-of-tenant", "Can read roles of tenant", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "1ce9908dba38490cbc65389bfeece21e", "d31184dabbc6435eaec86694650c2679", "Can read private roles", false, "can-read-private-roles", "Can read private roles", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "39d20cb8c4d541c6944aeeb678261cea", "d31184dabbc6435eaec86694650c2679", "Can create permissions", true, "create-permission", "Create permission", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "80ca0e41ea5046519f351a99b03b294e", "d31184dabbc6435eaec86694650c2679", "Can read invitations of tenant", true, "getall-invitation", "Can read invitations of tenant", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "8b1c2d303f3b45a1aa3ae6af46c8652b", "d31184dabbc6435eaec86694650c2679", "Can create roles", true, "create-role", "Can create role", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "aca002cfbf3a47899ff4c16e6be2c029", "d31184dabbc6435eaec86694650c2679", "Can read roles", true, "getall-role", "Can read roles", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "d1d34f71201f4b3e8f1c232aef35c40a", "d31184dabbc6435eaec86694650c2679", "Can read permissions of tenant", false, "can-read-tenants-permissions", "Can read permissions of tenant", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "d40ad347c7f943e399069eef409b4fa6", "d31184dabbc6435eaec86694650c2679", "Can read permissions", true, "getall-permission", "Can read permissions", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "e0132221c91f44ada257a38d951407d6", "d31184dabbc6435eaec86694650c2679", "Can read private permissions", false, "can-read-private-permissions", "Can read private permissions", "b22fcf202bd84a97936ccf2949e00da4" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { "5c2260fc58864b75a4cad5c0e7dd57cb", "d31184dabbc6435eaec86694650c2679", "Tenant Owner", false, true, "Tenant Owner", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "Id", "AccountId", "RoleId" },
                values: new object[] { "1f191c90510d456d84bda9e17fe24f50", "5a0d9e179991499e80db0a15fda4df79", "5c2260fc58864b75a4cad5c0e7dd57cb" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "6e3f476ec4354b27af25e025034ee97e", "aca002cfbf3a47899ff4c16e6be2c029", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "8d01aeac30dd45599da743bcc3f3ee0d", "e0132221c91f44ada257a38d951407d6", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "922a41f8597742178605a5ea7c75be32", "1ce9908dba38490cbc65389bfeece21e", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "a85e6d40858e4451b8a103bd903b6269", "05276f2a25dc4db5b37b0948e05c35ab", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "be097c9f1b4e4b3088172bcb0c75372b", "8b1c2d303f3b45a1aa3ae6af46c8652b", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "c07081abf2054ec496bee67b44a2ee2a", "39d20cb8c4d541c6944aeeb678261cea", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "d26570a4df1a41fea0bf0006f1b87721", "80ca0e41ea5046519f351a99b03b294e", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "d76afb762df349caadc39b7373ea98ed", "d40ad347c7f943e399069eef409b4fa6", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "e568c9eb81b24a5cae922c2a9a2ebc41", "d1d34f71201f4b3e8f1c232aef35c40a", "5c2260fc58864b75a4cad5c0e7dd57cb" },
                    { "fe7a5b81f9284af1857621c234ebc615", "1554a06426024ee88baabad7a71d65cf", "5c2260fc58864b75a4cad5c0e7dd57cb" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TenantId",
                table: "Accounts",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsApps_AccountId",
                table: "AccountsApps",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsApps_AppId",
                table: "AccountsApps",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsRoles_AccountId",
                table: "AccountsRoles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsRoles_RoleId",
                table: "AccountsRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_TenantId",
                table: "Apps",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AppId",
                table: "Permissions",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_TenantId",
                table: "Permissions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswords_AccountId",
                table: "ResetPasswords",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AppId",
                table: "Roles",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_TenantId",
                table: "Roles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_PermissionId",
                table: "RolesPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_RoleId",
                table: "RolesPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AccountId",
                table: "Sessions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AppId",
                table: "Sessions",
                column: "AppId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsApps");

            migrationBuilder.DropTable(
                name: "AccountsRoles");

            migrationBuilder.DropTable(
                name: "ResetPasswords");

            migrationBuilder.DropTable(
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
