using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.DataAccess.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddAppSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Apps",
                columns: new[] { "Id", "IsDefault", "Name", "TenantId" },
                values: new object[] { "d31184dabbc6435eaec86694650c2679", true, "Auth Provider WEB API", "b22fcf202bd84a97936ccf2949e00da4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Apps",
                keyColumn: "Id",
                keyValue: "d31184dabbc6435eaec86694650c2679");
        }
    }
}
