namespace SickLeaveApp.Application.DTOs;

public class CreateSickLeaveDto
{
    public string FullName { get; set; }
    public string Snils { get; set; }
    public decimal IncomeYear1 { get; set; }
    public decimal IncomeYear2 { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ExperienceYears { get; set; }
}