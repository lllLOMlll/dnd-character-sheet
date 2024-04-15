using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetDnD.Migrations
{
    /// <inheritdoc />
    public partial class TestWithoutArmor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterArmors",
                columns: table => new
                {
                    ArmorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    ArmorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ValueInGold = table.Column<int>(type: "int", nullable: false),
                    StealthDisadvantage = table.Column<bool>(type: "bit", nullable: false),
                    ArmorType = table.Column<int>(type: "int", nullable: false),
                    IsEquipped = table.Column<bool>(type: "bit", nullable: false),
                    IsMagicItem = table.Column<bool>(type: "bit", nullable: false),
                    RequiresAttunement = table.Column<bool>(type: "bit", nullable: false),
                    IsAttuned = table.Column<bool>(type: "bit", nullable: false),
                    MagicBonusAC = table.Column<int>(type: "int", nullable: false),
                    MagicEffectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MagicEffectMechanics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MagicCharges = table.Column<int>(type: "int", nullable: true),
                    MagicRechargeRate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterArmors", x => x.ArmorID);
                    table.ForeignKey(
                        name: "FK_CharacterArmors_Characters_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Characters",
                        principalColumn: "CharacterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterArmors_CharacterID",
                table: "CharacterArmors",
                column: "CharacterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterArmors");
        }
    }
}
