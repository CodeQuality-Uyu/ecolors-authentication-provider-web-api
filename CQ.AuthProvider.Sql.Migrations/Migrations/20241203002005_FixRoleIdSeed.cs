using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class FixRoleIdSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("216b14a3-337a-45a6-a75d-cae870a73918"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { new Guid("780a89b1-9fd3-4cf6-b802-2882ebb3db92"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Permissions over Auth Provider Web Api app", false, true, "Auth Provider Web Api Owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { new Guid("216b14a3-337a-45a6-a75d-cae870a73918"), new Guid("780a89b1-9fd3-4cf6-b802-2882ebb3db92") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("216b14a3-337a-45a6-a75d-cae870a73918"), new Guid("780a89b1-9fd3-4cf6-b802-2882ebb3db92") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("780a89b1-9fd3-4cf6-b802-2882ebb3db92"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AppId", "Description", "IsDefault", "IsPublic", "Name", "TenantId" },
                values: new object[] { new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc"), "Permissions over Auth Provider Web Api app", false, true, "Auth Provider Web Api Owner", new Guid("882a262c-e1a7-411d-a26e-40c61f3b810c") });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { new Guid("216b14a3-337a-45a6-a75d-cae870a73918"), new Guid("f4ad89eb-6a0b-427a-8aef-b6bc736884dc") });
        }
    }
}
