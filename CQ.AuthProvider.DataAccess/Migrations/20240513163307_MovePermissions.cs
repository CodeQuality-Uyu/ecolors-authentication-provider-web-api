using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MovePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsRoles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { "d47025648273495ba69482fcc69da874", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "21a73466810c4e99840e99e1a58e57df");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "4560660aa9474b1b9c3047d21cdecab2", "e6136901b87a47018f21407b72c013ca" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "654834a7cc234e098302d1bad925c3f9", "e6136901b87a47018f21407b72c013ca" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "886416f6b1e44dedac826662202010ea", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "8b9dea2caa3e4481b67cfbecfbbda6f6", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "8deb4e1b283f4d939d134c511dc65a6f", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "ab435b2ef92d4a96851e2fc0891fa492", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "ac4e7b6f39a24b98964dcc04b1b2b24e", "e6136901b87a47018f21407b72c013ca" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "b63d682602a34e0d8feaffbee83a693d", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "b9f9bc1b840f4b51b9f1fccb82c52944", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "c13551d59b354bff9878d99eae94e04f", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "d7c51933084c4facb88a9f27de33d01a", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "d887260f12c3419ea5ea656d842d8373", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "db00d1698409477f9e27101136983582", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "e8668a426cd546f3ac0b4ec7d1a67afd", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "eec8923fe738473793f38f6ba24d42a1", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "f8a2929a5ac44264b567121fc61fd8ca", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "4560660aa9474b1b9c3047d21cdecab2");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "654834a7cc234e098302d1bad925c3f9");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "886416f6b1e44dedac826662202010ea");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "8b9dea2caa3e4481b67cfbecfbbda6f6");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "8deb4e1b283f4d939d134c511dc65a6f");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "ab435b2ef92d4a96851e2fc0891fa492");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "ac4e7b6f39a24b98964dcc04b1b2b24e");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "b63d682602a34e0d8feaffbee83a693d");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "b9f9bc1b840f4b51b9f1fccb82c52944");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "c13551d59b354bff9878d99eae94e04f");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "d7c51933084c4facb88a9f27de33d01a");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "d887260f12c3419ea5ea656d842d8373");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "db00d1698409477f9e27101136983582");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "e8668a426cd546f3ac0b4ec7d1a67afd");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "eec8923fe738473793f38f6ba24d42a1");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "f8a2929a5ac44264b567121fc61fd8ca");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2aeae9e48ce740e7bf0ab8647e05761e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e6136901b87a47018f21407b72c013ca");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "d47025648273495ba69482fcc69da874",
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2024, 5, 13, 16, 33, 7, 249, DateTimeKind.Unspecified).AddTicks(6694), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "IsPublic", "Key", "Name" },
                values: new object[,]
                {
                    { "210d8698918c40c8b8e302c35a3523e5", "Obtener todos los permisos", false, "getall-permission", "Obtener permisos" },
                    { "3444dbe534ea488991dd10501355a925", "Obtener permisos privados", false, "getallprivate-permission", "Obtener permisos privados" },
                    { "4122d0b4b2db40bea6417a27f013ae46", "Obtener todos los roles", false, "getall-role", "Obtener roles" },
                    { "4f2f35c3c49d4aaca161b18607874f7c", "Obtener un rol", false, "getbyid-role", "Obtener un rol" },
                    { "6342efeaac844e159deefa3727ca4bab", "Actualizar un permiso", false, "updatebyid-permission", "Actualizar un permiso" },
                    { "7044ad611b1340baade95ea771c2c705", "Actualizar un rol", false, "addpermission-role", "Actualizar un rol" },
                    { "77423e9af2fd4eacb5054356f2f9981c", "Crear client system", false, "create-clientsystem", "Crear client system" },
                    { "7df82153fefd4c12b54f85a6cb0eedc6", "Crear muchos roles", false, "createbulk-role", "Crear muchos roles" },
                    { "8046303599db476eaa62cf9a4873680c", "Crear permiso", false, "create-permission", "Crear permiso" },
                    { "a5d958d6acb947d3ba591ba2bd3ac9f6", "Crear cuenta para un usuario", false, "createcredentialsfor-account", "Crear cuenta para un usuario" },
                    { "cd52f7af4a5a4e39b2cdec5f7ca8f6e3", "Joker", false, "*", "Joker" },
                    { "d2f2a0c007e149778e00109c77acf87e", "Validar token", false, "validatetoken-session", "Validar token" },
                    { "da48186536fe4656a93627aa6403b202", "Obtener roles privados", false, "getallprivate-role", "Obtener roles privados" },
                    { "e25d0b1160c14d3485999bd9f9ab5490", "Obtener un permiso", false, "getbyid-permission", "Obtener un permiso" },
                    { "f043868bcbd74ccab60ccf434c7da0c4", "Obtener permisos de un rol", false, "getallbyroleid-permission", "Obtener permisos de un rol" },
                    { "f416d065609b4df28ca26cf8dfa1180c", "Crear rol", false, "create-role", "Crear rol" },
                    { "f6dfd1dee30042a3b4b719c5f9d4861b", "Crear muchos permisos", false, "createbulk-permission", "Crear muchos permisos" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "IsDefault", "IsPublic", "Key", "Name" },
                values: new object[,]
                {
                    { "28d3a06deaa4432ea1fec0b399261291", "Admin", false, false, "admin", "Admin" },
                    { "6031973784584a03a372a0a63f270657", "Client System", false, false, "clientSystem", "Client System" }
                });

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { "d47025648273495ba69482fcc69da874", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "210d8698918c40c8b8e302c35a3523e5", "28d3a06deaa4432ea1fec0b399261291" },
                    { "3444dbe534ea488991dd10501355a925", "28d3a06deaa4432ea1fec0b399261291" },
                    { "4122d0b4b2db40bea6417a27f013ae46", "28d3a06deaa4432ea1fec0b399261291" },
                    { "4f2f35c3c49d4aaca161b18607874f7c", "28d3a06deaa4432ea1fec0b399261291" },
                    { "6342efeaac844e159deefa3727ca4bab", "28d3a06deaa4432ea1fec0b399261291" },
                    { "7044ad611b1340baade95ea771c2c705", "28d3a06deaa4432ea1fec0b399261291" },
                    { "77423e9af2fd4eacb5054356f2f9981c", "28d3a06deaa4432ea1fec0b399261291" },
                    { "7df82153fefd4c12b54f85a6cb0eedc6", "28d3a06deaa4432ea1fec0b399261291" },
                    { "8046303599db476eaa62cf9a4873680c", "28d3a06deaa4432ea1fec0b399261291" },
                    { "a5d958d6acb947d3ba591ba2bd3ac9f6", "28d3a06deaa4432ea1fec0b399261291" },
                    { "d2f2a0c007e149778e00109c77acf87e", "6031973784584a03a372a0a63f270657" },
                    { "da48186536fe4656a93627aa6403b202", "28d3a06deaa4432ea1fec0b399261291" },
                    { "e25d0b1160c14d3485999bd9f9ab5490", "28d3a06deaa4432ea1fec0b399261291" },
                    { "f043868bcbd74ccab60ccf434c7da0c4", "28d3a06deaa4432ea1fec0b399261291" },
                    { "f416d065609b4df28ca26cf8dfa1180c", "28d3a06deaa4432ea1fec0b399261291" },
                    { "f6dfd1dee30042a3b4b719c5f9d4861b", "28d3a06deaa4432ea1fec0b399261291" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsRoles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { "d47025648273495ba69482fcc69da874", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "cd52f7af4a5a4e39b2cdec5f7ca8f6e3");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "210d8698918c40c8b8e302c35a3523e5", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "3444dbe534ea488991dd10501355a925", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "4122d0b4b2db40bea6417a27f013ae46", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "4f2f35c3c49d4aaca161b18607874f7c", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "6342efeaac844e159deefa3727ca4bab", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "7044ad611b1340baade95ea771c2c705", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "77423e9af2fd4eacb5054356f2f9981c", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "7df82153fefd4c12b54f85a6cb0eedc6", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "8046303599db476eaa62cf9a4873680c", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "a5d958d6acb947d3ba591ba2bd3ac9f6", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "d2f2a0c007e149778e00109c77acf87e", "6031973784584a03a372a0a63f270657" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "da48186536fe4656a93627aa6403b202", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "e25d0b1160c14d3485999bd9f9ab5490", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "f043868bcbd74ccab60ccf434c7da0c4", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "f416d065609b4df28ca26cf8dfa1180c", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "f6dfd1dee30042a3b4b719c5f9d4861b", "28d3a06deaa4432ea1fec0b399261291" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "210d8698918c40c8b8e302c35a3523e5");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "3444dbe534ea488991dd10501355a925");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "4122d0b4b2db40bea6417a27f013ae46");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "4f2f35c3c49d4aaca161b18607874f7c");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "6342efeaac844e159deefa3727ca4bab");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "7044ad611b1340baade95ea771c2c705");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "77423e9af2fd4eacb5054356f2f9981c");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "7df82153fefd4c12b54f85a6cb0eedc6");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "8046303599db476eaa62cf9a4873680c");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "a5d958d6acb947d3ba591ba2bd3ac9f6");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "d2f2a0c007e149778e00109c77acf87e");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "da48186536fe4656a93627aa6403b202");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "e25d0b1160c14d3485999bd9f9ab5490");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "f043868bcbd74ccab60ccf434c7da0c4");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "f416d065609b4df28ca26cf8dfa1180c");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "f6dfd1dee30042a3b4b719c5f9d4861b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "28d3a06deaa4432ea1fec0b399261291");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6031973784584a03a372a0a63f270657");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: "d47025648273495ba69482fcc69da874",
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2024, 3, 27, 19, 22, 56, 542, DateTimeKind.Unspecified).AddTicks(5654), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "IsPublic", "Key", "Name" },
                values: new object[,]
                {
                    { "21a73466810c4e99840e99e1a58e57df", "Joker", false, "*", "Joker" },
                    { "4560660aa9474b1b9c3047d21cdecab2", "Crear muchos roles", false, "createbulk-role", "Crear muchos roles" },
                    { "654834a7cc234e098302d1bad925c3f9", "Crear muchos permisos", false, "createbulk-permission", "Crear muchos permisos" },
                    { "886416f6b1e44dedac826662202010ea", "Crear permiso", false, "create-permission", "Crear permiso" },
                    { "8b9dea2caa3e4481b67cfbecfbbda6f6", "Obtener todos los roles", false, "getall-role", "Obtener roles" },
                    { "8deb4e1b283f4d939d134c511dc65a6f", "Obtener todos los permisos", false, "getall-permission", "Obtener permisos" },
                    { "ab435b2ef92d4a96851e2fc0891fa492", "Obtener permisos privados", false, "getallprivate-permission", "Obtener permisos privados" },
                    { "ac4e7b6f39a24b98964dcc04b1b2b24e", "Validar token", false, "validatetoken-session", "Validar token" },
                    { "b63d682602a34e0d8feaffbee83a693d", "Crear cuenta para un usuario", false, "createcredentialsfor-account", "Crear cuenta para un usuario" },
                    { "b9f9bc1b840f4b51b9f1fccb82c52944", "Crear client system", false, "create-clientsystem", "Crear client system" },
                    { "c13551d59b354bff9878d99eae94e04f", "Obtener un permiso", false, "getbyid-permission", "Obtener un permiso" },
                    { "d7c51933084c4facb88a9f27de33d01a", "Obtener permisos de un rol", false, "getallbyroleid-permission", "Obtener permisos de un rol" },
                    { "d887260f12c3419ea5ea656d842d8373", "Actualizar un rol", false, "addpermission-role", "Actualizar un rol" },
                    { "db00d1698409477f9e27101136983582", "Crear rol", false, "create-role", "Crear rol" },
                    { "e8668a426cd546f3ac0b4ec7d1a67afd", "Obtener un rol", false, "getbyid-role", "Obtener un rol" },
                    { "eec8923fe738473793f38f6ba24d42a1", "Actualizar un permiso", false, "updatebyid-permission", "Actualizar un permiso" },
                    { "f8a2929a5ac44264b567121fc61fd8ca", "Obtener roles privados", false, "getallprivate-role", "Obtener roles privados" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "IsDefault", "IsPublic", "Key", "Name" },
                values: new object[,]
                {
                    { "2aeae9e48ce740e7bf0ab8647e05761e", "Admin", false, false, "admin", "Admin" },
                    { "e6136901b87a47018f21407b72c013ca", "Client System", false, false, "clientSystem", "Client System" }
                });

            migrationBuilder.InsertData(
                table: "AccountsRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { "d47025648273495ba69482fcc69da874", "2aeae9e48ce740e7bf0ab8647e05761e" });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "4560660aa9474b1b9c3047d21cdecab2", "e6136901b87a47018f21407b72c013ca" },
                    { "654834a7cc234e098302d1bad925c3f9", "e6136901b87a47018f21407b72c013ca" },
                    { "886416f6b1e44dedac826662202010ea", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "8b9dea2caa3e4481b67cfbecfbbda6f6", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "8deb4e1b283f4d939d134c511dc65a6f", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "ab435b2ef92d4a96851e2fc0891fa492", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "ac4e7b6f39a24b98964dcc04b1b2b24e", "e6136901b87a47018f21407b72c013ca" },
                    { "b63d682602a34e0d8feaffbee83a693d", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "b9f9bc1b840f4b51b9f1fccb82c52944", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "c13551d59b354bff9878d99eae94e04f", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "d7c51933084c4facb88a9f27de33d01a", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "d887260f12c3419ea5ea656d842d8373", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "db00d1698409477f9e27101136983582", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "e8668a426cd546f3ac0b4ec7d1a67afd", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "eec8923fe738473793f38f6ba24d42a1", "2aeae9e48ce740e7bf0ab8647e05761e" },
                    { "f8a2929a5ac44264b567121fc61fd8ca", "2aeae9e48ce740e7bf0ab8647e05761e" }
                });
        }
    }
}
