using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterSheetDnD.Models
{
	public enum Rarity
	{
		Common,
		Uncommon,
		Rare,
		VeryRare,
		Legendary,
		Artifact,
		Other
	}

	public enum ArmorType
	{ 
		Light,
		Medium,
		Heavy,
		Shield
	}

	public enum LigthArmor
	{
		Padded,
		Leather,
		StuddedLeather
	}

	public enum MediumArmor
	{
		Hide,
		ChainShirt,
		ScaleMail,
		Breatplate,
		HalfPlate
	}

	public enum HeavyArmor
	{
		RingMail,
		ChainMail,
		Splin,
		FullPlate		
	}

	public enum MagicBonusAC
	{
		[Display(Name = "+0")]
		None = 0,

		[Display(Name = "+1")]
		B1 = 1,

		[Display(Name = "+2")]
		B2 = 2,

		[Display(Name = "+3")]
		B3 = 3,

		[Display(Name = "+4")]
		B4 = 4,

		[Display(Name = "+5")]
		B5 = 5,

	}

	public class Armor 
	{
	
		public bool StealthDisadvantage { get; set; }
		public ArmorType ArmorType { get; set; }
		public LigthArmor LigthArmor { get; set; }
		public MediumArmor MediumArmor { get; set; }
		public HeavyArmor HeavyArmor { get; set; }
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
}
