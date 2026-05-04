namespace SickLeaveApp.Domain.Entities;

public class IncomeRecord
{
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    public int Year { get; private set; }
    public decimal Amount { get; private set; }
    
    protected IncomeRecord() { }
    
    public IncomeRecord(Guid employeeId, int year, decimal amount)
    {
        if (year < 2000 || year > DateTime.Now.Year)
            throw new ArgumentException("Указан некорректный год для учета доходов.");

        if (amount < 0)
            throw new ArgumentException("Сумма дохода не может быть отрицательной.");

        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        Year = year;
        Amount = amount;
    }
}