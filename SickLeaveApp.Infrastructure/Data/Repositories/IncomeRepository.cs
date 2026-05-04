using SickLeaveApp.Domain.Entities;

namespace SickLeaveApp.Infrastructure.Data.Repositories;

public class IncomeRepository
{
    private readonly AppDbContext _context;

    public IncomeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public List<IncomeRecord> GetIncomesByEmployeeId(Guid employeeId)
    {
        return _context.IncomeRecords
            .Where(i => i.EmployeeId == employeeId)
            .ToList();
    }

    public void Add(IncomeRecord income)
    {
        _context.IncomeRecords.Add(income);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}