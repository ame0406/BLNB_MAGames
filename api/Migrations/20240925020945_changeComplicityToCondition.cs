using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class changeComplicityToCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Complicity_ComponentId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Complicity");

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Condition_ComponentId",
                table: "Stocks",
                column: "ComponentId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Condition_ComponentId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.CreateTable(
                name: "Complicity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complicity", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Complicity_ComponentId",
                table: "Stocks",
                column: "ComponentId",
                principalTable: "Complicity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
