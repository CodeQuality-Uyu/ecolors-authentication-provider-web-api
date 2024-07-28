using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQ.IdentityProvider.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Identities\"");

            migrationBuilder
                .InsertData("Identities",
                ["Id", "Email", "Password"],
                ["5f7dd4f88608458ea68bdc3ef9a94e59", "codequality@tenant.com", "!12345678"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Identities\"");
        }
    }
}
