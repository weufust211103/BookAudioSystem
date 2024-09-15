using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAudioSystem.Migrations
{
    /// <inheritdoc />
    public partial class test_migration_ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$b2ThHxOhb1JD/Er7KWmMiOKwjFRuuDcBCke0CEKYpzBd4.1bxFzL2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "+dwF6bayal0rgck0HsNQ06FCtKmlF+pkO/Nh3BU7JhA=");
        }
    }
}
