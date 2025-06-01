using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class baseObjId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Base_Obj_GameId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Condition_ComponentId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ComponentId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Stocks",
                newName: "BaseObjId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_GameId",
                table: "Stocks",
                newName: "IX_Stocks_BaseObjId");

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedSalePrice",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ConditionId",
                table: "Stocks",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Base_Obj_BaseObjId",
                table: "Stocks",
                column: "BaseObjId",
                principalTable: "Base_Obj",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Condition_ConditionId",
                table: "Stocks",
                column: "ConditionId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Base_Obj_BaseObjId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Condition_ConditionId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ConditionId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "EstimatedSalePrice",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "BaseObjId",
                table: "Stocks",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_BaseObjId",
                table: "Stocks",
                newName: "IX_Stocks_GameId");

            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ComponentId",
                table: "Stocks",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Base_Obj_GameId",
                table: "Stocks",
                column: "GameId",
                principalTable: "Base_Obj",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Condition_ComponentId",
                table: "Stocks",
                column: "ComponentId",
                principalTable: "Condition",
                principalColumn: "Id");
        }
    }
}
