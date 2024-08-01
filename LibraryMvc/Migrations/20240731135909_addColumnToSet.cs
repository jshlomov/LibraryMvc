using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMvc.Migrations
{
    /// <inheritdoc />
    public partial class addColumnToSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Set",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Set");
        }
    }
}
