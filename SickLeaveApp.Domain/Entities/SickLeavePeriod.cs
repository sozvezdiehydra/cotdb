using SickLeaveApp.Domain.Enums;

namespace SickLeaveApp.Domain.Entities;

public class SickLeavePeriod
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; private set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SickLeaveReason Reason { get; private set; }
    
    protected SickLeavePeriod() { }
    
    public SickLeavePeriod(Guid employeeId, DateTime startDate, DateTime endDate, SickLeaveReason reason)
    {
        if (endDate < startDate)
            throw new ArgumentException("Дата окончания больничного не может быть раньше даты начала.");

        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        StartDate = startDate.Date; 
        EndDate = endDate.Date;
        Reason = reason;
    }
    
    public int TotalDays => (EndDate - StartDate).Days + 1;
    
    public int EmployerDays 
    {
        get 
        {
            if (Reason == SickLeaveReason.Illness || Reason == SickLeaveReason.Injury)
            {
                return Math.Min(TotalDays, 3);
            }
                
            return 0;
        }
    }
    
    public int SfrDays => TotalDays - EmployerDays;
    
}