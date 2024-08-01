using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMvc.Migrations
{
    /// <inheritdoc />
    public partial class Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Set_SetId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Shelf_ShelfId",
                table: "Book");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Set_SetId",
                table: "Book",
                column: "SetId",
                principalTable: "Set",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Shelf_ShelfId",
                table: "Book",
                column: "ShelfId",
                principalTable: "Shelf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Set_SetId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Shelf_ShelfId",
                table: "Book");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Set_SetId",
                table: "Book",
                column: "SetId",
                principalTable: "Set",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Shelf_ShelfId",
                table: "Book",
                column: "ShelfId",
                principalTable: "Shelf",
                principalColumn: "Id");
        }
    }
}
