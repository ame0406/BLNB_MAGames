using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addsplitpriceTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BuyPriceForWhoToWhoIsTrue",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrixDachatForWhoToWhoIsTrue",
                table: "Lots",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyPriceForWhoToWhoIsTrue",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "PrixDachatForWhoToWhoIsTrue",
                table: "Lots");
        }
    }
}
