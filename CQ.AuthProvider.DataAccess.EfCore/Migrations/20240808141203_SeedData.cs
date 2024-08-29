using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .DropForeignKey(
                name: "FK_Accounts_Tenants_TenantId",
                table: "Accounts");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "FullName", "LastName", "Locale", "ProfilePictureUrl", "TenantId", "TimeZone" },
                values: new object[] { "5a0d9e179991499e80db0a15fda4df79", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "seed@cq.com", "Seed", "Seed Seed", "Seed", "Uruguay", null, "b22fcf202bd84a97936ccf2949e00da4", "-3" });

            migrationBuilder
                .DropForeignKey(
                name: "FK_Permissions_Tenants_TenantId",
                table: "Permissions");

            migrationBuilder
                .DropForeignKey(
                name: "FK_Permissions_Apps_AppId",
                table: "Permissions");
            
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AppId", "Description", "IsPublic", "Key", "Name", "TenantId" },
                values: new object[,]
                {
                    { "920d910719224496b575618a9495d2c4", "d31184dabbc6435eaec86694650c2679", "Full accesss", false, "full-access", "Full access", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "e2d42874c56e46319b21eeb817f3b988", "d31184dabbc6435eaec86694650c2679", "Joker", false, "*", "Joker", "b22fcf202bd84a97936ccf2949e00da4" }
                });

            migrationBuilder
                .DropForeignKey(
                name: "FK_Roles_Tenants_TenantId",
                table: "Roles");
            
            migrationBuilder
                .DropForeignKey(
                name: "FK_Roles_Apps_AppId",
                table: "Roles");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { "4c00f792d8ed43768846711094902d8c", "d31184dabbc6435eaec86694650c2679", "Auth API Owner", true, false, "Auth API Owner", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder
                .DropForeignKey(
                name: "FK_AccountsApps_Apps_AppId",
                table: "AccountsApps");

            migrationBuilder.InsertData(
                table: "AccountsApps",
                columns: new[] { "Id", "AccountId", "AppId" },
                values: new object[] { "ef03980ea2a54e4bba92e022fbd33d9b", "5a0d9e179991499e80db0a15fda4df79", "d31184dabbc6435eaec86694650c2679" });

            migrationBuilder
                .DropForeignKey(
                name: "FK_AccountsRoles_Tenants_TenantId",
                table: "AccountsRoles");

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "Id", "AccountId", "RoleId", "TenantId" },
                values: new object[] { "9dc669b28b0f4f3fb8a832961a76a6c9", "5a0d9e179991499e80db0a15fda4df79", "4c00f792d8ed43768846711094902d8c", "b22fcf202bd84a97936ccf2949e00da4" });

            migrationBuilder
                .DropForeignKey(
                name: "FK_RolesPermissions_Tenants_TenantId",
                table: "RolesPermissions");

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "Id", "PermissionId", "RoleId", "TenantId" },
                values: new object[,]
                {
                    { "4909564462b040289d0dc0758cf8942e", "e2d42874c56e46319b21eeb817f3b988", "4c00f792d8ed43768846711094902d8c", "b22fcf202bd84a97936ccf2949e00da4" },
                    { "64ec1b6bbd3d4c49b609c0f58359e7ac", "e2d42874c56e46319b21eeb817f3b988", "4c00f792d8ed43768846711094902d8c", "b22fcf202bd84a97936ccf2949e00da4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsApps",
                keyColumn: "Id",
                keyValue: "ef03980ea2a54e4bba92e022fbd33d9b");

            migrationBuilder.DeleteData(
                table: "AccountsRoles",
                keyColumn: "Id",
                keyValue: "9dc669b28b0f4f3fb8a832961a76a6c9");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "920d910719224496b575618a9495d2c4");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "4909564462b040289d0dc0758cf8942e");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumn: "Id",
                keyValue: "64ec1b6bbd3d4c49b609c0f58359e7ac");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "5a0d9e179991499e80db0a15fda4df79");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "e2d42874c56e46319b21eeb817f3b988");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4c00f792d8ed43768846711094902d8c");
        }
    }
}
