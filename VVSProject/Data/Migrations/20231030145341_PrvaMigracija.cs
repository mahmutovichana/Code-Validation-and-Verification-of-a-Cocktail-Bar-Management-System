using Microsoft.EntityFrameworkCore.Migrations;

namespace VVSProject.Data.Migrations
{
    public partial class PrvaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredient_idDrink",
                table: "DrinkIngredient",
                column: "idDrink");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkIngredient_idIngredient",
                table: "DrinkIngredient",
                column: "idIngredient");

            migrationBuilder.CreateIndex(
                name: "IX_Statistic_idDrink",
                table: "Statistic",
                column: "idDrink");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkIngredient");

            migrationBuilder.DropTable(
                name: "Statistic");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Drink");
        }
    }
}
