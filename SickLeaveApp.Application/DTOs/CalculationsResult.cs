namespace SickLeaveApp.Application.DTOs;

public class CalculationsResult
{
    public decimal TotalAllowedIncome { get; set; }
    public decimal AverageDailyEarnings { get; set; }
    public decimal DailyAllowance { get; set; }
    public decimal EmployerAmount { get; set; }
    public decimal SfrAmount { get; set; }
    public decimal TotalAmount => EmployerAmount + SfrAmount;
}