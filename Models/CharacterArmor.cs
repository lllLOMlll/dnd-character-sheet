using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
	public enum Rarity
	{
		Common, Uncommon, Rare, VeryRare, Legendary, Artifact, Other
	}

	public enum ArmorType
	{
		Light, Medium, Heavy, Shield
	}

	public enum MagicBonusAC
	{
		[Display(Name = "+0")] None = 0,
		[Display(Name = "+1")] B1 = 1,
		[Display(Name = "+2")] B2 = 2,
		[Display(Name = "+3")] B3 = 3,
		[Display(Name = "+4")] B4 = 4,
		[Display(Name = "+5")] B5 = 5,
	}

	// Enums for Polyphormism
	public enum LightArmorType { Padded, Leather, StuddedLeather }
	public enum MediumArmorType { Hide, ChainShirt, ScaleMail, Breastplate, HalfPlate }
	public enum HeavyArmorType { RingMail, ChainMail, Splint, Plate }

	public abstract class CharacterArmor
	{
		[Key]
		public int ArmorID { get; set; }

		[ForeignKey("CharacterID")]
		public int CharacterID { get; set; }
		public virtual Character? Character { get; set; }

		[Required(ErrorMessage = "Armor name is required.")]
		public string? ArmorName { get; set; }
		public Rarity Rarity { get; set; }
		public string? Description { get; set; }
		public int Quantity { get; set; }
		public int ValueInGold { get; set; }
		public bool StealthDisadvantage { get; set; }
		public ArmorType ArmorType { get; set; }
		public bool IsEquipped { get; set; }
		public bool IsMagicItem { get; set; }
		public bool RequiresAttunement { get; set; }
		public bool IsAttuned { get; set; }
		public MagicBonusAC MagicBonusAC { get; set; }
		public string? MagicEffectDescription { get; set; }
		public string? MagicEffectMechanics { get; set; }
		[Range(0, 100, ErrorMessage = "Charges must be a positive number")]
		public int? MagicCharges { get; set; }
		public string? MagicRechargeRate { get; set; }
	}



	public class LightArmor : CharacterArmor
	{
		public LightArmorType LightArmorType { get; set; }
		// Additional properties specific to light armor can be added here
	}

	public class MediumArmor : CharacterArmor
	{
		public MediumArmorType MediumArmorType { get; set; }
		// Additional properties specific to medium armor can be added here
	}

	public class HeavyArmor : CharacterArmor
	{
		public HeavyArmorType HeavyArmorType { get; set; }
		// Additional properties specific to heavy armor can be added here
	}

	public class ShieldArmor : CharacterArmor
	{
		// Shields might have specific properties that could be added here
	}

}
