using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetDnD.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentType2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEquipmentBases_Characters_CharacterID1",
                table: "CharacterEquipmentBases");

            migrationBuilder.DropIndex(
                name: "IX_CharacterEquipmentBases_CharacterID1",
                table: "CharacterEquipmentBases");

            migrationBuilder.DropColumn(
                name: "CharacterID1",
                table: "CharacterEquipmentBases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterID1",
                table: "CharacterEquipmentBases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEquipmentBases_CharacterID1",
                table: "CharacterEquipmentBases",
                column: "CharacterID1");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEquipmentBases_Characters_CharacterID1",
                table: "CharacterEquipmentBases",
                column: "CharacterID1",
                principalTable: "Characters",
                principalColumn: "CharacterID");
        }
    }
}
