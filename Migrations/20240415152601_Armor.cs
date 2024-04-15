using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetDnD.Migrations
{
    /// <inheritdoc />
    public partial class Armor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeavyArmorType",
                table: "CharacterArmors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LightArmorType",
                table: "CharacterArmors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MediumArmorType",
                table: "CharacterArmors",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeavyArmorType",
                table: "CharacterArmors");

            migrationBuilder.DropColumn(
                name: "LightArmorType",
                table: "CharacterArmors");

            migrationBuilder.DropColumn(
                name: "MediumArmorType",
                table: "CharacterArmors");
        }
    }
}
