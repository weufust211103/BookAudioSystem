using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookAudioSystem.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SoldDate",
                table: "Transactions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookID = table.Column<int>(type: "integer", nullable: false),
                    BuyerID = table.Column<int>(type: "integer", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_BuyerID",
                        column: x => x.BuyerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "createdDate" },
                values: new object[] { "$2a$11$K7kKKUCQeNGm7leZpSDr6u4Af/ENMThcseG73dovI60Q72SA28Z0e", new DateTime(2024, 10, 30, 15, 40, 34, 363, DateTimeKind.Utc).AddTicks(9947) });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BookID",
                table: "Orders",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerID",
                table: "Orders",
                column: "BuyerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropColumn(
                name: "SoldDate",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "createdDate" },
                values: new object[] { "$2a$11$dZLnihfSZVYyPRmp3nORp.XWCBXActpTY0Gc75GHo8eSLaQbeKWMO", new DateTime(2024, 10, 30, 14, 51, 44, 573, DateTimeKind.Utc).AddTicks(5396) });
        }
    }
}
