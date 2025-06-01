using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class q : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_Obj_Marques_MarqueId",
                table: "Base_Obj");

            migrationBuilder.AddColumn<decimal>(
                name: "PrixDachat",
                table: "Lots",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "MarqueId",
                table: "Base_Obj",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Obj_Marques_MarqueId",
                table: "Base_Obj",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_Obj_Marques_MarqueId",
                table: "Base_Obj");

            migrationBuilder.DropColumn(
                name: "PrixDachat",
                table: "Lots");

            migrationBuilder.AlterColumn<int>(
                name: "MarqueId",
                table: "Base_Obj",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Obj_Marques_MarqueId",
                table: "Base_Obj",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
