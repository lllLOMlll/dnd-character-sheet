using CharacterSheetDnD.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CharacterSheetDnD.Utilities
{
	public static class ConvertEnumForDisplayUtility
	{
		public static string GetDisplayName(Enum enumValue)
		{
			var memberInfo = enumValue.GetType()
						   .GetMember(enumValue.ToString())
						   .FirstOrDefault(); // Use FirstOrDefault to avoid exceptions

			if (memberInfo == null)
			{		
				return "Melee"; 
			}

			// If we found a member, proceed as before
			var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
			return displayAttribute?.GetName() ?? enumValue.ToString();
		}

		public static string GetDisplayNamesForFlags(Enum flagsEnum)
		{
			var type = flagsEnum.GetType();
			var flagValues = Enum.GetValues(type).Cast<Enum>();
			var selectedFlags = flagValues.Where(flagsEnum.HasFlag).Select(flag => flag.GetType()
																						.GetMember(flag.ToString())
																						.First()
																						.GetCustomAttribute<DisplayAttribute>()
																						?.GetName() ?? flag.ToString());
			return string.Join(", ", selectedFlags);
		}

		// This method directly gets the display name for a given enum value, which is suitable for non-flags enums.
		public static string GetDisplayNameForNonFlagsEnum(Enum enumValue)
		{
			var memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
			if (memberInfo != null)
			{
				var attribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
				return attribute?.GetName() ?? enumValue.ToString();
			}
			return enumValue.ToString();
		}

		public static string GetNumericValue(Enum enumValue)
		{
			// Directly parsing the enum's name, assuming it's like "B1", "B2", etc.
			var name = enumValue.ToString();
			if (name.StartsWith("B"))
			{
				return name.Substring(1); // Remove the first character and return the rest
			}
			return name; // Fallback in case the naming convention isn't followed
		}

	}


}

