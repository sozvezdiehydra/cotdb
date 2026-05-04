namespace SickLeaveApp.Domain.Constants;

public static class SfrLimits
{
    private static readonly Dictionary<int, decimal> YearlyLimits = new Dictionary<int, decimal>
    {
        { 2021, 966000m },
        { 2022, 1032000m },
        { 2023, 1917000m },
        { 2024, 2225000m },
        { 2025, 2759000m } 
    };
    
    public static decimal GetAllowedIncome(int year, decimal actualIncome)
    {
        // if there is a limit for the year in the dictionary, we take it,
        // if not, we count it without a limit.
        if (YearlyLimits.TryGetValue(year, out decimal limit))
        {
            return actualIncome > limit ? limit : actualIncome;
        }
            
        return actualIncome;
    }
}