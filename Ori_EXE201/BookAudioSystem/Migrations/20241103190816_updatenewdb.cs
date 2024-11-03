using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookAudioSystem.Migrations
{
    /// <inheritdoc />
    public partial class updatenewdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_OwnerID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_OwnerID",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Transactions",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
        name: "NewTransactionID",
        table: "Transactions",
        type: "text",
        nullable: false,
        defaultValue: "");
            migrationBuilder.Sql("UPDATE \"Transactions\" SET \"NewTransactionID\" = \"TransactionID\"::text");
            migrationBuilder.DropColumn(
        name: "TransactionID",
        table: "Transactions");

            // Step 4: Rename the new column to the original name
            migrationBuilder.RenameColumn(
                name: "NewTransactionID",
                table: "Transactions",
                newName: "TransactionID");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "createdDate" },
                values: new object[] { "$2a$11$S4nZX95fABV9A2tCdx.hLOD0o3lV4X866TPaKNma1RirlkWFpgZ8u", new DateTime(2024, 11, 3, 19, 8, 16, 33, DateTimeKind.Utc).AddTicks(1231) });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OrderId",
                table: "Transactions",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Orders_OrderId",
                table: "Transactions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Orders_OrderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_OrderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Transactions",
                newName: "OwnerID");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionID",
                table: "Transactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "createdDate" },
                values: new object[] { "$2a$11$K7kKKUCQeNGm7leZpSDr6u4Af/ENMThcseG73dovI60Q72SA28Z0e", new DateTime(2024, 10, 30, 15, 40, 34, 363, DateTimeKind.Utc).AddTicks(9947) });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OwnerID",
                table: "Transactions",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_OwnerID",
                table: "Transactions",
                column: "OwnerID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
