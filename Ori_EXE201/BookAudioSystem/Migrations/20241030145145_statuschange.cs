using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAudioSystem.Migrations
{
    /// <inheritdoc />
    public partial class statuschange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Books",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "createdDate" },
                values: new object[] { "$2a$11$dZLnihfSZVYyPRmp3nORp.XWCBXActpTY0Gc75GHo8eSLaQbeKWMO", new DateTime(2024, 10, 30, 14, 51, 44, 573, DateTimeKind.Utc).AddTicks(5396) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Books",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "createdDate" },
                values: new object[] { "$2a$11$8EC4CvwLNNvrElK6Injd.eQd2bdkJqiMzgHI0RiDeIWuePUYYtbBS", new DateTime(2024, 10, 3, 3, 15, 36, 756, DateTimeKind.Utc).AddTicks(8901) });
        }
    }
}
