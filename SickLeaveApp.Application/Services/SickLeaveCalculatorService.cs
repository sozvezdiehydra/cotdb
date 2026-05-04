using SickLeaveApp.Domain.Constants;
using SickLeaveApp.Domain.Entities;
using SickLeaveApp.Application.DTOs;

namespace SickLeaveApp.Application.Services;

public class SickLeaveCalculatorService
{
    public CalculationsResult Calculate(Employee employee, SickLeavePeriod sickLeave, List<IncomeRecord> incomes)
    {
        int year1 = sickLeave.StartDate.Year - 2;
        int year2 = sickLeave.StartDate.Year - 1;
        
        decimal incomeYear1 = incomes.FirstOrDefault(i => i.Year == year1)?.Amount ?? 0;
        decimal incomeYear2 = incomes.FirstOrDefault(i => i.Year == year2)?.Amount ?? 0;
        
        decimal allowedIncome1 = SfrLimits.GetAllowedIncome(year1, incomeYear1);
        decimal allowedIncome2 = SfrLimits.GetAllowedIncome(year2, incomeYear2);

        decimal totalAllowedIncome = allowedIncome1 + allowedIncome2;
        
        decimal averageDailyEarnings = totalAllowedIncome / 730m;
        
        decimal dailyAllowance = averageDailyEarnings * employee.PaymentMultiplier;
        
        decimal employerAmount = dailyAllowance * sickLeave.EmployerDays;
        decimal sfrAmount = dailyAllowance * sickLeave.SfrDays;
        
        return new CalculationsResult
        {
            TotalAllowedIncome = totalAllowedIncome,
            AverageDailyEarnings = decimal.Round(averageDailyEarnings, 2), // Округляем до копеек
            DailyAllowance = decimal.Round(dailyAllowance, 2),
            EmployerAmount = decimal.Round(employerAmount, 2),
            SfrAmount = decimal.Round(sfrAmount, 2)
        };
    }
}