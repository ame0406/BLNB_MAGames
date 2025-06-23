using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class DisableAllCascadeDeletes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_Obj_Marques_MarqueId",
                table: "Base_Obj");

            migrationBuilder.DropForeignKey(
                name: "FK_Base_Obj_SaleType_SaleTypeId",
                table: "Base_Obj");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjImages_Base_Obj_Base_ObjId",
                table: "ObjImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Base_Obj_BaseObjId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Condition_ConditionId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Lots_LotId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Status_StatusId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksVenteEbay_Stocks_StocksId",
                table: "StocksVenteEbay");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksVenteEbay_VenteEbay_VentesEbayId",
                table: "StocksVenteEbay");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksVenteMKP_Stocks_StocksId",
                table: "StocksVenteMKP");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksVenteMKP_VenteMKP_VentesMKPId",
                table: "StocksVenteMKP");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Obj_Marques_MarqueId",
                table: "Base_Obj",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Obj_SaleType_SaleTypeId",
                table: "Base_Obj",
                column: "SaleTypeId",
                principalTable: "SaleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjImages_Base_Obj_Base_ObjId",
                table: "ObjImages",
                column: "Base_ObjId",
                principalTable: "Base_Obj",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Base_Obj_BaseObjId",
                table: "Stocks",
                column: "BaseObjId",
                principalTable: "Base_Obj",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Condition_ConditionId",
                table: "Stocks",
                column: "ConditionId",
                principalTable: "Condition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Lots_LotId",
                table: "Stocks",
                column: "LotId",
                principalTable: "Lots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Status_StatusId",
                table: "Stocks",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksVenteEbay_Stocks_StocksId",
                table: "StocksVenteEbay",
                column: "StocksId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksVenteEbay_VenteEbay_VentesEbayId",
                table: "StocksVenteEbay",
                column: "VentesEbayId",
                principalTable: "VenteEbay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksVenteMKP_Stocks_StocksId",
                table: "StocksVenteMKP",
                column: "StocksId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksVenteMKP_VenteMKP_VentesMKPId",
                table: "StocksVenteMKP",
                column: "VentesMKPId",
                principalTable: "VenteMKP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_Obj_Marques_MarqueId",
                table: "Base_Obj");

            migrationBuilder.DropForeignKey(
                name: "FK_Base_Obj_SaleType_SaleTypeId",
                table: "Base_Obj");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjImages_Base_Obj_Base_ObjId",
                table: "ObjImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Base_Obj_BaseObjId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Condition_ConditionId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Lots_LotId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Status_StatusId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksVenteEbay_Stocks_StocksId",
                table: "StocksVenteEbay");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksVenteEbay_VenteEbay_VentesEbayId",
                table: "StocksVenteEbay");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksVenteMKP_Stocks_StocksId",
                table: "StocksVenteMKP");

            migrationBuilder.DropForeignKey(
                name: "FK_StocksVenteMKP_VenteMKP_VentesMKPId",
                table: "StocksVenteMKP");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Obj_Marques_MarqueId",
                table: "Base_Obj",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Obj_SaleType_SaleTypeId",
                table: "Base_Obj",
                column: "SaleTypeId",
                principalTable: "SaleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjImages_Base_Obj_Base_ObjId",
                table: "ObjImages",
                column: "Base_ObjId",
                principalTable: "Base_Obj",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Lots_LotId",
                table: "Stocks",
                column: "LotId",
                principalTable: "Lots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Status_StatusId",
                table: "Stocks",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksVenteEbay_Stocks_StocksId",
                table: "StocksVenteEbay",
                column: "StocksId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksVenteEbay_VenteEbay_VentesEbayId",
                table: "StocksVenteEbay",
                column: "VentesEbayId",
                principalTable: "VenteEbay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksVenteMKP_Stocks_StocksId",
                table: "StocksVenteMKP",
                column: "StocksId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StocksVenteMKP_VenteMKP_VentesMKPId",
                table: "StocksVenteMKP",
                column: "VentesMKPId",
                principalTable: "VenteMKP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
