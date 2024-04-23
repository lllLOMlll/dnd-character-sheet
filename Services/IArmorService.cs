using CharacterSheetDnD.Models;
using System.Collections.Generic;

namespace CharacterSheetDnD.Services
{
	public interface IArmorService
	{
		List<CharacterArmor> GetEquippedArmors(int characterId);
		int CalculateTotalAC(int characterId, int dexterityModifier);
	}
}
