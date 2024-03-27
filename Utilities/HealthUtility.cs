using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CharacterSheetDnD.Utilities
{
	public class HealthUtility
	{
		public static string ChangeButtonColorCurrentHitPoints(int currentHitPoints, int maximumHitPoints)
		{
			// Ensure you pass maximumHitPoints to calculate the percentage correctly
			double healthPercentage = (double)currentHitPoints / maximumHitPoints * 100;

			if (healthPercentage > 80)
			{
				return "btn-success";
			}
			else if (healthPercentage <= 80 && healthPercentage > 30)
			{
				return "btn-warning";
			}
			else if (healthPercentage <= 30 && healthPercentage > 0)
			{
				return "btn-danger";
			}
			else if (healthPercentage == 0)
			{
				return "btn-dark";
			}
			else
			{
				return "btn-light";
			}
		}
	}
}
