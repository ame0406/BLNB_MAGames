using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class changement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Lots_LotId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_LotId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "LotId",
                table: "Stocks");

            migrationBuilder.CreateTable(
                name: "LotsStocks",
                columns: table => new
                {
                    LotsId = table.Column<int>(type: "int", nullable: false),
                    StocksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotsStocks", x => new { x.LotsId, x.StocksId });
                    table.ForeignKey(
                        name: "FK_LotsStocks_Lots_LotsId",
                        column: x => x.LotsId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotsStocks_Stocks_StocksId",
                        column: x => x.StocksId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotsStocks_StocksId",
                table: "LotsStocks",
                column: "StocksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotsStocks");

            migrationBuilder.AddColumn<int>(
                name: "LotId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_LotId",
                table: "Stocks",
                column: "LotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Lots_LotId",
                table: "Stocks",
                column: "LotId",
                principalTable: "Lots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
