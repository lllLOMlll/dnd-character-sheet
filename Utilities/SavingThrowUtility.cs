public static class SavingThrowUtility
{
    public static int CalculateSavingThrowModifier(bool isProficient, int abilityScoreModifier, int proficiencyBonus)
    {
        return abilityScoreModifier + (isProficient ? proficiencyBonus : 0);
    }
}

