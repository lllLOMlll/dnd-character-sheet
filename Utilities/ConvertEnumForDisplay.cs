using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CharacterSheetDnD.Utilities
{
	public static class ConvertEnumForDisplay
	{
		public static string GetDisplayName(Enum enumValue)
		{
			return enumValue.GetType()
				   .GetMember(enumValue.ToString())
				   .First()
				   .GetCustomAttribute<DisplayAttribute>()?
				   .GetName() ?? enumValue.ToString();
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

	}
}

