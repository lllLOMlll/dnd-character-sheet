using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetDnD.Migrations
{
    /// <inheritdoc />
    public partial class Equipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterEquipmentBases",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsEquipped = table.Column<bool>(type: "bit", nullable: false),
                    IsMagicItem = table.Column<bool>(type: "bit", nullable: false),
                    ValueInGold = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    ArmorClass = table.Column<int>(type: "int", nullable: true),
                    StealthDisadvantage = table.Column<bool>(type: "bit", nullable: true),
                    DamageDice = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DamageType = table.Column<int>(type: "int", nullable: true),
                    Range = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEquipmentBases", x => x.EquipmentID);
                    table.ForeignKey(
                        name: "FK_CharacterEquipmentBases_Characters_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Characters",
                        principalColumn: "CharacterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MagicItems",
                columns: table => new
                {
                    MagicItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentID = table.Column<int>(type: "int", nullable: true),
                    EffectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectMechanics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Charges = table.Column<int>(type: "int", nullable: true),
                    RechargeRate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagicItems", x => x.MagicItemID);
                    table.ForeignKey(
                        name: "FK_MagicItems_CharacterEquipmentBases_EquipmentID",
                        column: x => x.EquipmentID,
                        principalTable: "CharacterEquipmentBases",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEquipmentBases_CharacterID",
                table: "CharacterEquipmentBases",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_MagicItems_EquipmentID",
                table: "MagicItems",
                column: "EquipmentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MagicItems");

            migrationBuilder.DropTable(
                name: "CharacterEquipmentBases");
        }
    }
}
