using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMvc.Migrations
{
    /// <inheritdoc />
    public partial class addHightToSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hight",
                table: "Set",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hight",
                table: "Set");
        }
    }
}
