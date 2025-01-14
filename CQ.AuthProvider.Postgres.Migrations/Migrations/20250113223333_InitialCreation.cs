using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.Postgres.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MiniLogoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoverLogoId = table.Column<Guid>(type: "uuid", nullable: false),
                    WebUrl = table.Column<string>(type: "text", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ProfilePictureId = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Locale = table.Column<string>(type: "text", nullable: false),
                    TimeZone = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CoverId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsApps", x => new { x.AppId, x.AccountId });
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    AppId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    AppId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AppId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsRoles", x => new { x.RoleId, x.AccountId });
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
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Accounts_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitations_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.RoleId, x.PermissionId });
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
                columns: new[] { "Id", "CoverLogoId", "MiniLogoId", "Name", "OwnerId", "WebUrl" },
                values: new object[] { new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "Seed Tenant", new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"), null });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "FullName", "LastName", "Locale", "ProfilePictureId", "TenantId", "TimeZone" },
                values: new object[] { new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "seed@cq.com", "Seed", "Seed Seed", "Seed", "Uruguay", null, new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c"), "-3" });

            migrationBuilder.InsertData(
                table: "Apps",
                columns: new[] { "Id", "CoverId", "IsDefault", "Name", "TenantId" },
                values: new object[] { new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), new Guid("00000000-0000-0000-0000-000000000000"), true, "Auth Provider Web Api", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });

            migrationBuilder.InsertData(
                table: "AccountsApps",
                columns: new[] { "AccountId", "AppId" },
                values: new object[] { new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc") });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[,]
                {
                    { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create accounts", true, "createcredentialsfor-account", "Can create accounts", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("06f5a862-9cfd-4c1f-a777-4c4b3adb916b"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can update tenant name", true, "updatetenantname-me", "Can update tenant name", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("0b2f5e97-42f9-4e56-9ee2-40b033cff9e8"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create invitations", true, "create-invitation", "Can create invitations", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("216b14a3-337a-45a6-a75d-cae870a73918"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read all tenants", true, "getall-tenants", "Can read all tenants", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("27c1378d-39df-4a57-b025-fc96963955a6"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read all accounts", true, "getall-account", "Can read all accounts", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("2eab3c3a-792a-444a-97f3-01db00dffcab"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create app", true, "create-app", "Can create app", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("32b32564-459f-4e74-8456-83147bd03c9e"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create permissions", true, "create-permission", "Create permission", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("40bc0960-8c55-488e-a014-f5b52db3d306"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read invitations of tenant", true, "getall-invitation", "Can read invitations of tenant", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("45104ffc-433c-42bc-a887-18d71d2bc524"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create tenant", true, "create-tenant", "Can create tenant", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("6323b5da-b78c-4984-a56e-8206775d3e91"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read apps of tenant", true, "getall-app", "Can read apps", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can update tenant owner", true, "transfertenant-me", "Can update tenant owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("bcb925af-f4be-4782-978c-c496b044609f"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read permissions", true, "getall-permission", "Can read permissions", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can create roles", true, "create-role", "Can create role", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Can read roles", true, "getall-role", "Can read roles", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[,]
                {
                    { new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Should be deleted once deployed", false, false, "Seed", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("780a89b1-9fd3-4cf6-b802-2882ebb3db92"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Permissions over Auth Provider Web Api app", false, true, "Auth Provider Web Api Owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") },
                    { new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Tenant Owner", false, true, "Tenant Owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") }
                });

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { new Guid("0ee82ee9-f480-4b13-ad68-579dc83dfa0d"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") },
                    { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") },
                    { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("77f7ff91-a807-43ac-bc76-1b34c52c5345") },
                    { new Guid("216b14a3-337a-45a6-a75d-cae870a73918"), new Guid("780a89b1-9fd3-4cf6-b802-2882ebb3db92") },
                    { new Guid("046c65a8-d3c1-41d7-bda2-a96d393cc18e"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("06f5a862-9cfd-4c1f-a777-4c4b3adb916b"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("0b2f5e97-42f9-4e56-9ee2-40b033cff9e8"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("27c1378d-39df-4a57-b025-fc96963955a6"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("2eab3c3a-792a-444a-97f3-01db00dffcab"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("32b32564-459f-4e74-8456-83147bd03c9e"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("40bc0960-8c55-488e-a014-f5b52db3d306"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("45104ffc-433c-42bc-a887-18d71d2bc524"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("6323b5da-b78c-4984-a56e-8206775d3e91"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("a43d40d7-7aa6-4abb-a124-890d7218ac86"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("bcb925af-f4be-4782-978c-c496b044609f"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("ec6141a0-d0f7-4102-b41c-c8d50a86e3a9"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") },
                    { new Guid("fc598ab0-1f14-4224-a187-4556a9926f6f"), new Guid("cf4a209a-8dbd-4dac-85d9-ed899424b49e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TenantId",
                table: "Accounts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsApps_AccountId",
                table: "AccountsApps",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsRoles_AccountId",
                table: "AccountsRoles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_TenantId",
                table: "Apps",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_AppId",
                table: "Invitations",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_CreatorId",
                table: "Invitations",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_RoleId",
                table: "Invitations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_TenantId",
                table: "Invitations",
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
                name: "Invitations");

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
