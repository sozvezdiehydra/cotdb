using SickLeaveApp.Domain.Entities;

namespace SickLeaveApp.Domain.Repositories;

public interface IEmployeeRepository
{
    // get employee by id
    Employee GetById(Guid id);
    
    // get employee by snils
    Employee GetBySnils(string snils);
    
    IEnumerable<Employee> GetAll();
    
    void Add(Employee employee);
    
    void Update(Employee employee);
    
    // save changes in DB
    void SaveChanges();
}