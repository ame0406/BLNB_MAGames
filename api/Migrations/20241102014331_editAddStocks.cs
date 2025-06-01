using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class editAddStocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Condition_ComponentId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "comments",
                table: "Stocks",
                newName: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ManualRate",
                table: "Stocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "Stocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CDRate",
                table: "Stocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyPrice",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "BoxRate",
                table: "Stocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoldDate",
                table: "Stocks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SoldPrice",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Condition_ComponentId",
                table: "Stocks",
                column: "ComponentId",
                principalTable: "Condition",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Condition_ComponentId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SoldDate",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SoldPrice",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "Stocks",
                newName: "comments");

            migrationBuilder.AlterColumn<int>(
                name: "ManualRate",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CDRate",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyPrice",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BoxRate",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Condition_ComponentId",
                table: "Stocks",
                column: "ComponentId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
