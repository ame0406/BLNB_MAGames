using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addMarquesTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_ConsoleSystem_ConsoleId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Games_GameId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_SaleType_SaleTypeId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "ConsoleSystem");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "LotsStocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ConsoleId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_SaleTypeId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ConsoleId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SaleTypeId",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "LotId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Marques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_Obj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoSpace = table.Column<int>(type: "int", nullable: true),
                    TypeObj = table.Column<short>(type: "smallint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SaleTypeId = table.Column<int>(type: "int", nullable: false),
                    MarqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_Obj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Base_Obj_Marques_MarqueId",
                        column: x => x.MarqueId,
                        principalTable: "Marques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Base_Obj_SaleType_SaleTypeId",
                        column: x => x.SaleTypeId,
                        principalTable: "SaleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObjImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Base_ObjId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjImages_Base_Obj_Base_ObjId",
                        column: x => x.Base_ObjId,
                        principalTable: "Base_Obj",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_LotId",
                table: "Stocks",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Base_Obj_MarqueId",
                table: "Base_Obj",
                column: "MarqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Base_Obj_SaleTypeId",
                table: "Base_Obj",
                column: "SaleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjImages_Base_ObjId",
                table: "ObjImages",
                column: "Base_ObjId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Base_Obj_GameId",
                table: "Stocks",
                column: "GameId",
                principalTable: "Base_Obj",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Lots_LotId",
                table: "Stocks",
                column: "LotId",
                principalTable: "Lots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Base_Obj_GameId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Lots_LotId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "ObjImages");

            migrationBuilder.DropTable(
                name: "Base_Obj");

            migrationBuilder.DropTable(
                name: "Marques");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_LotId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "LotId",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "ConsoleId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SaleTypeId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ConsoleSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsoleSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

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
                name: "IX_Stocks_ConsoleId",
                table: "Stocks",
                column: "ConsoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SaleTypeId",
                table: "Stocks",
                column: "SaleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LotsStocks_StocksId",
                table: "LotsStocks",
                column: "StocksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_ConsoleSystem_ConsoleId",
                table: "Stocks",
                column: "ConsoleId",
                principalTable: "ConsoleSystem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Games_GameId",
                table: "Stocks",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_SaleType_SaleTypeId",
                table: "Stocks",
                column: "SaleTypeId",
                principalTable: "SaleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
