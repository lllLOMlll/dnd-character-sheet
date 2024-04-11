using CharacterSheetDnD.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CharacterSheetDnD.Utilities
{


	public static class WeaponPropertyUtility
	{
		// Method to check if the "Finesse" flag is set in a WeaponProperty enum value
		public static bool HasFinesse(WeaponProperty properties)
		{
			// Check if the Finesse flag is included in the properties
			return properties.HasFlag(WeaponProperty.Finesse);
		}

		public static bool HasThrown(WeaponProperty properties)
		{
			return properties.HasFlag(WeaponProperty.Thrown);
		}

	}


}

