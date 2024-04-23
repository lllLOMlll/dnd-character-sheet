using CharacterSheetDnD.Models;
using System.Collections.Generic;

namespace CharacterSheetDnD.Services
{
	public interface IArmorService
	{
		int CalculateTotalAC(int characterId, int dexterityModifier);
	}
}
