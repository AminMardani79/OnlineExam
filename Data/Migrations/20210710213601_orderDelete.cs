using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class orderDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailModels_OrderModel_OrderId",
                table: "OrderDetailModels");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailModels_OrderModel_OrderId",
                table: "OrderDetailModels",
                column: "OrderId",
                principalTable: "OrderModel",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailModels_OrderModel_OrderId",
                table: "OrderDetailModels");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailModels_OrderModel_OrderId",
                table: "OrderDetailModels",
                column: "OrderId",
                principalTable: "OrderModel",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
