using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetDnD.Migrations
{
    /// <inheritdoc />
    public partial class WeaponValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Range",
                table: "Weapons",
                newName: "WeaponRange");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeaponRange",
                table: "Weapons",
                newName: "Range");
        }
    }
}
