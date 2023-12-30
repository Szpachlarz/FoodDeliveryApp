using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryApp.Migrations
{
    /// <inheritdoc />
    public partial class minor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_DishCategories_CategoryId",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Dishes",
                newName: "DishCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_CategoryId",
                table: "Dishes",
                newName: "IX_Dishes_DishCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_DishCategories_DishCategoryId",
                table: "Dishes",
                column: "DishCategoryId",
                principalTable: "DishCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_DishCategories_DishCategoryId",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "DishCategoryId",
                table: "Dishes",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_DishCategoryId",
                table: "Dishes",
                newName: "IX_Dishes_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_DishCategories_CategoryId",
                table: "Dishes",
                column: "CategoryId",
                principalTable: "DishCategories",
                principalColumn: "Id");
        }
    }
}
