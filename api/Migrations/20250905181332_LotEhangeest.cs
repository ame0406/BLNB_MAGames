using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class LotEhangeest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LotEchangeId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_LotEchangeId",
                table: "Stocks",
                column: "LotEchangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Lots_LotEchangeId",
                table: "Stocks",
                column: "LotEchangeId",
                principalTable: "Lots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Lots_LotEchangeId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_LotEchangeId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "LotEchangeId",
                table: "Stocks");
        }
    }
}
