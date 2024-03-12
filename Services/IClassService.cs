namespace CharacterSheetDnD.Services
{
    public interface IClassService
    {
        IEnumerable<string> GetAvailableClasses();
    }

    public class ClassService : IClassService
    {
        public IEnumerable<string> GetAvailableClasses()
        {
            // This list could come from a database, configuration file, or be statically defined
            return new List<string> { "Wizard", "Fighter", "Rogue", "Cleric", "Paladin", "Ranger", "Warlock", "Barbarian", "Sorcerer", "Druid", "Monk", "Bard" };
        }
    }
}
