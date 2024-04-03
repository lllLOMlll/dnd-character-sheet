using CharacterSheetDnD.Models;
using CharacterSheetDnD.Utilities;

namespace CharacterSheetDnD.ViewModels
{
    public class EquipmentAddViewModel
    {
        public int CharacterID { get; set; } // To know which character you're adding equipment to
        public string? ItemName { get; set; }
        public EquipmentType? ItemType { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsEquipped { get; set; }
        public int ValueInGold { get; set; } = 0;
    }
}


