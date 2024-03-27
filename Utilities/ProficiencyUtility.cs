namespace CharacterSheetDnD.Utilities
{
	public class ProficiencyUtility
	{
		public static int GetProficiency(int level)
		{
			if (level > 0 && level < 5) // Levels 1-4
			{
				return 2;
			}
			else if (level < 9) // Covers levels 5-8
			{
				return 3;
			}
			else if (level < 13) // Covers levels 9-12
			{
				return 4;
			}
			else if (level < 17) // Covers levels 13-16
			{
				return 5;
			}
			else if (level < 21) // Covers levels 17-20
			{
				return 6;
			}
			else // Default or error case, adjust as necessary
			{
				return -1; 
			}
		}
	}
}
