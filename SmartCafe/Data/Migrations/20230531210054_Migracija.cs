using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCafe.Data.Migrations
{
    public partial class Migracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bartender",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bartender", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Drink",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drink", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tableNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Statistic",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    totalProfit = table.Column<double>(type: "float", nullable: false),
                    dailyProfit = table.Column<double>(type: "float", nullable: false),
                    idDrink = table.Column<int>(type: "int", nullable: false),
                    noOfEmployees = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistic", x => x.id);
                    table.ForeignKey(
                        name: "FK_Statistic_Drink_idDrink",
                        column: x => x.idDrink,
                        principalTable: "Drink",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    done = table.Column<bool>(type: "bit", nullable: false),
                    tableNumber = table.Column<int>(type: "int", nullable: false),
                    orderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idGuest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_Guest_idGuest",
                        column: x => x.idGuest,
                        principalTable: "Guest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrinkIngredient",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDrink = table.Column<int>(type: "int", nullable: false),
                    idIngredient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkIngredient", x => x.id);
                    table.ForeignKey(
                        name: "FK_DrinkIngredient_Drink_idDrink",
                        column: x => x.idDrink,
                        principalTable: "Drink",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkIngredient_Ingredient_idIngredient",
                        column: x => x.idIngredient,
                        principalTable: "Ingredient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDrink = table.Column<int>(type: "int", nullable: false),
                    idOrder = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Drink_idDrink",
                        column: x => x.idDrink,
                        principalTable: "Drink",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_idOrder",
                        column: x => x.idOrder,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredient_idDrink",
                table: "DrinkIngredient",
                column: "idDrink");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredient_idIngredient",
                table: "DrinkIngredient",
                column: "idIngredient");

            migrationBuilder.CreateIndex(
                name: "IX_Order_idGuest",
                table: "Order",
                column: "idGuest");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_idDrink",
                table: "OrderItem",
                column: "idDrink");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_idOrder",
                table: "OrderItem",
                column: "idOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Statistic_idDrink",
                table: "Statistic",
                column: "idDrink");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bartender");

            migrationBuilder.DropTable(
                name: "DrinkIngredient");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Statistic");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Drink");

            migrationBuilder.DropTable(
                name: "Guest");
        }
    }
}