using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAudioSystem.Migrations
{
    /// <inheritdoc />
    public partial class DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "createdDate" },
                values: new object[] { "$2a$11$6RQNxotpvHlN9J4yFRqPmuxF3nePVxLPOMx6g8EAD9CJ2PNaZxQeu", new DateTime(2024, 11, 18, 19, 35, 54, 96, DateTimeKind.Utc).AddTicks(1885) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "createdDate" },
                values: new object[] { "$2a$11$S4nZX95fABV9A2tCdx.hLOD0o3lV4X866TPaKNma1RirlkWFpgZ8u", new DateTime(2024, 11, 3, 19, 8, 16, 33, DateTimeKind.Utc).AddTicks(1231) });
        }
    }
}
