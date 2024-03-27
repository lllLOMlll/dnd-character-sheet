using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CharacterSheetDnD.Migrations
{
    /// <inheritdoc />
    public partial class SavingThrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavingThrows",
                columns: table => new
                {
                    SavingThrowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingThrows", x => x.SavingThrowID);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSavingThrows",
                columns: table => new
                {
                    CharacterSavingThrowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    SavingThrowID = table.Column<int>(type: "int", nullable: false),
                    IsProficient = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSavingThrows", x => x.CharacterSavingThrowID);
                    table.ForeignKey(
                        name: "FK_CharacterSavingThrows_Characters_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Characters",
                        principalColumn: "CharacterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterSavingThrows_SavingThrows_SavingThrowID",
                        column: x => x.SavingThrowID,
                        principalTable: "SavingThrows",
                        principalColumn: "SavingThrowID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SavingThrows",
                columns: new[] { "SavingThrowID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Used for physical tasks and resisting physical force", "Strength" },
                    { 2, "Used for agility, reflexes, and balance tasks", "Dexterity" },
                    { 3, "Used for endurance and health", "Constitution" },
                    { 4, "Used for memory and reasoning", "Intelligence" },
                    { 5, "Used for perception and insight", "Wisdom" },
                    { 6, "Used for interaction and leadership", "Charisma" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSavingThrows_CharacterID",
                table: "CharacterSavingThrows",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSavingThrows_SavingThrowID",
                table: "CharacterSavingThrows",
                column: "SavingThrowID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterSavingThrows");

            migrationBuilder.DropTable(
                name: "SavingThrows");
        }
    }
}
