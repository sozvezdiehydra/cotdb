using SickLeaveApp.Domain.Entities;
using SickLeaveApp.Domain.Repositories;

namespace SickLeaveApp.Infrastructure.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    // DI
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public Employee GetById(Guid id)
    {
        return _context.Employees.FirstOrDefault(e => e.Id == id);
    }

    public Employee GetBySnils(string snils)
    {
        return _context.Employees.FirstOrDefault(e => e.Snils == snils);
    }

    public IEnumerable<Employee> GetAll()
    {
        return _context.Employees.ToList();
    }

    public void Add(Employee employee)
    {
        _context.Employees.Add(employee);
    }

    public void Update(Employee employee)
    {
        _context.Employees.Update(employee);
    }

    public void SaveChanges()
    {
        // save in DB
        _context.SaveChanges();
    }
}