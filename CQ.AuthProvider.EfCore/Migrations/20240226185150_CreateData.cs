using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.AuthProvider.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class CreateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Identities",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[] { "d4702564-8273-495b-a694-82fcc69da874", "admin@gmail.com", "!12345678" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Identities",
                keyColumn: "Id",
                keyValue: "d4702564-8273-495b-a694-82fcc69da874");
        }
    }
}
