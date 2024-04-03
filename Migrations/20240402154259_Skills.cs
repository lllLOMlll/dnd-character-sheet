using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharacterSheetDnD.Migrations
{
    /// <inheritdoc />
    public partial class Skills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSkills",
                columns: table => new
                {
                    CharacterSkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false),
                    IsProficient = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSkills", x => x.CharacterSkillID);
                    table.ForeignKey(
                        name: "FK_CharacterSkills_Characters_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Characters",
                        principalColumn: "CharacterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSkills_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Dexterity checks for staying on your feet in tricky situations.", "Acrobatics" },
                    { 2, "Wisdom checks for calming down animals, understanding their intentions, or predicting their actions.", "Animal Handling" },
                    { 3, "Intelligence checks for recalling lore about spells, magic items, eldritch symbols, magical traditions, the planes of existence, and the inhabitants of those planes.", "Arcana" },
                    { 4, "Strength checks for climbing, jumping, or swimming.", "Athletics" },
                    { 5, "Charisma checks when you try to lie, bluff, or mislead someone.", "Deception" },
                    { 6, "Intelligence checks for recalling lore about historical events, legendary people, ancient kingdoms, past disputes, recent wars, and lost civilizations.", "History" },
                    { 7, "Wisdom checks for determining the true intentions of a creature, such as when searching out a lie or predicting someone’s next move.", "Insight" },
                    { 8, "Charisma checks for influencing someone through overt threats, hostile actions, and physical violence.", "Intimidation" },
                    { 9, "Intelligence checks for looking around for clues and making deductions based on those clues.", "Investigation" },
                    { 10, "Wisdom checks for stabilizing a dying companion or diagnosing an illness.", "Medicine" },
                    { 11, "Intelligence checks for recalling lore about terrain, plants and animals, the weather, and natural cycles.", "Nature" },
                    { 12, "Wisdom checks for noticing things.", "Perception" },
                    { 13, "Charisma checks for delighting an audience with music, dance, acting, storytelling, or some other form of entertainment.", "Performance" },
                    { 14, "Charisma checks for influencing someone or a group of people with tact, social graces, or good nature.", "Persuasion" },
                    { 15, "Intelligence checks for recalling lore about deities, rites and prayers, religious hierarchies, holy symbols, and the practices of secret cults.", "Religion" },
                    { 16, "Dexterity checks for tasks like planting something on someone else or concealing an object on your person.", "Sleight of Hand" },
                    { 17, "Dexterity checks for hiding or moving silently.", "Stealth" },
                    { 18, "Wisdom checks for following tracks, hunting wild game, guiding your group through frozen wastelands, identifying signs that owlbears live nearby, predicting the weather, or avoiding quicksand and other natural hazards.", "Survival" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_CharacterID",
                table: "CharacterSkills",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_SkillID",
                table: "CharacterSkills",
                column: "SkillID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSkills");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
