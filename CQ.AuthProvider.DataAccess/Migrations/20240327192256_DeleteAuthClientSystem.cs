using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQ.AuthProvider.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAuthClientSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountsRoles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { "d47025648273495ba69482fcc69da874", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "ClientSystems",
                keyColumn: "Id",
                keyValue: "c7af32e7bbc34e26a72924c0d3ad9aad");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "85c4f11e41c64468a9eb4b657d7f992d");

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "0031a0bc0aac4e2a9cd94ed9796bbee8", "6565ebacc3a94007873967d083e9def0" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "073b1c6e9ddf4ebd9528aaa64d938916", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "0a0027c493804ee39499684aabc36c6d", "bcc1589004f54f0e91e67295a46a9140" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "105bb9d29d0f4928b6dceb7d9da744ba", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "1e5792159aeb465fba1368f7ff835e2c", "6565ebacc3a94007873967d083e9def0" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "243e9036a6b542478e068e56a30e11d1", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "5f83e7a5640e4eb4aa7f4c3e0b6985ac", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "6ca46b22fcd74070a55d883682991637", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "7d97d5a3a3624a94a9dcde6aa2a55339", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "9af1865df2fc487b8456c39625988ced", "6565ebacc3a94007873967d083e9def0" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "b3d1e8d0e72b4bef864417feec140d7c", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "b67bdf587d50422f907d065d9e709bf9", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "d750f3ec5a824cd8995bd981facab2bc", "6565ebacc3a94007873967d083e9def0" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "da51cccd7517426ca78d8c6bbec9026c", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "ed6dd49edba74f5da4f898ec6c860481", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "ef577b64f9c741f6b68a2732db49f2e5", "bcc1589004f54f0e91e67295a46a9140" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "f0b3321ff1c6426092c398ff3e4110f2", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "f16e3d89c187463791da4625fdb14589", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "RolesPermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { "f1ae8a44eb954f5083d4f48dbd321525", "71bcf11229094af4bd29239f6e850688" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "0031a0bc0aac4e2a9cd94ed9796bbee8");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "073b1c6e9ddf4ebd9528aaa64d938916");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "0a0027c493804ee39499684aabc36c6d");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "105bb9d29d0f4928b6dceb7d9da744ba");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "1e5792159aeb465fba1368f7ff835e2c");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "243e9036a6b542478e068e56a30e11d1");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "5f83e7a5640e4eb4aa7f4c3e0b6985ac");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "6ca46b22fcd74070a55d883682991637");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "7d97d5a3a3624a94a9dcde6aa2a55339");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "9af1865df2fc487b8456c39625988ced");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "b3d1e8d0e72b4bef864417feec140d7c");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "b67bdf587d50422f907d065d9e709bf9");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "d750f3ec5a824cd8995bd981facab2bc");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "da51cccd7517426ca78d8c6bbec9026c");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "ed6dd49edba74f5da4f898ec6c860481");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "ef577b64f9c741f6b68a2732db49f2e5");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "f0b3321ff1c6426092c398ff3e4110f2");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "f16e3d89c187463791da4625fdb14589");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: "f1ae8a44eb954f5083d4f48dbd321525");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6565ebacc3a94007873967d083e9def0");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "71bcf11229094af4bd29239f6e850688");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bcc1589004f54f0e91e67295a46a9140");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: new DateTimeOffset(new DateTime(2024, 3, 27, 17, 59, 27, 894, DateTimeKind.Unspecified).AddTicks(9841), new TimeSpan(0, 0, 0, 0, 0)));

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
        }
    }
}
