using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetDnD.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSavingThrows_SavingThrows_SavingThrowID",
                table: "CharacterSavingThrows");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkills_Skills_SkillID",
                table: "CharacterSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_MagicItems_CharacterEquipmentBases_EquipmentID",
                table: "MagicItems");

            migrationBuilder.AddColumn<int>(
                name: "CharacterID1",
                table: "CharacterEquipmentBases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CharacterID2",
                table: "CharacterEquipmentBases",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSavingThrows_SavingThrows_SavingThrowID",
                table: "CharacterSavingThrows",
                column: "SavingThrowID",
                principalTable: "SavingThrows",
                principalColumn: "SavingThrowID");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkills_Skills_SkillID",
                table: "CharacterSkills",
                column: "SkillID",
                principalTable: "Skills",
                principalColumn: "SkillID");

            migrationBuilder.AddForeignKey(
                name: "FK_MagicItems_CharacterEquipmentBases_EquipmentID",
                table: "MagicItems",
                column: "EquipmentID",
                principalTable: "CharacterEquipmentBases",
                principalColumn: "EquipmentID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEquipmentBases_Characters_CharacterID1",
                table: "CharacterEquipmentBases");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSavingThrows_SavingThrows_SavingThrowID",
                table: "CharacterSavingThrows");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkills_Skills_SkillID",
                table: "CharacterSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_MagicItems_CharacterEquipmentBases_EquipmentID",
                table: "MagicItems");

            migrationBuilder.DropIndex(
                name: "IX_CharacterEquipmentBases_CharacterID1",
                table: "CharacterEquipmentBases");

            migrationBuilder.DropColumn(
                name: "CharacterID1",
                table: "CharacterEquipmentBases");

            migrationBuilder.DropColumn(
                name: "CharacterID2",
                table: "CharacterEquipmentBases");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSavingThrows_SavingThrows_SavingThrowID",
                table: "CharacterSavingThrows",
                column: "SavingThrowID",
                principalTable: "SavingThrows",
                principalColumn: "SavingThrowID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkills_Skills_SkillID",
                table: "CharacterSkills",
                column: "SkillID",
                principalTable: "Skills",
                principalColumn: "SkillID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MagicItems_CharacterEquipmentBases_EquipmentID",
                table: "MagicItems",
                column: "EquipmentID",
                principalTable: "CharacterEquipmentBases",
                principalColumn: "EquipmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
