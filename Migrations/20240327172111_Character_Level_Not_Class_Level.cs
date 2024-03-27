using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetDnD.Migrations
{
    /// <inheritdoc />
    public partial class Character_Level_Not_Class_Level : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Characters");
        }
    }
}
