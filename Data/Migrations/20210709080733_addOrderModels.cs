using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addOrderModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TestPrice",
                table: "TestModels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "OrderModel",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFinally = table.Column<bool>(type: "bit", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderModel", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderModel_StudentModels_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentModels",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailModels",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailModels", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetailModels_OrderModel_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderModel",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetailModels_TestModels_TestId",
                        column: x => x.TestId,
                        principalTable: "TestModels",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailModels_OrderId",
                table: "OrderDetailModels",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailModels_TestId",
                table: "OrderDetailModels",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderModel_StudentId",
                table: "OrderModel",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailModels");

            migrationBuilder.DropTable(
                name: "OrderModel");

            migrationBuilder.DropColumn(
                name: "TestPrice",
                table: "TestModels");
        }
    }
}
