using System.Text.RegularExpressions;

namespace SickLeaveApp.Domain.Entities;

public class Employee
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Snils { get; private set; }
    public int ExperienceYears { get; private set; }
    public int ExperienceMonths { get; private set; }
    
    protected Employee() { }
    
    public Employee(string fullName, string snils, int experienceYears, int experienceMonths)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("ФИО не может быть пустым.");

        if (experienceYears < 0 || experienceMonths < 0 || experienceMonths > 11)
            throw new ArgumentException("Некорректно указан страховой стаж.");

        Id = Guid.NewGuid();
        FullName = fullName;
        SetSnils(snils);
            
        ExperienceYears = experienceYears;
        ExperienceMonths = experienceMonths;
    }
    
    public void SetSnils(string snils)
    {
        if (string.IsNullOrWhiteSpace(snils))
            throw new ArgumentException("СНИЛС не может быть пустым.");
        
        var cleanSnils = Regex.Replace(snils, @"\D", "");
            
        if (cleanSnils.Length != 11)
            throw new ArgumentException("СНИЛС должен содержать ровно 11 цифр.");
        
        Snils = $"{cleanSnils.Substring(0, 3)}-{cleanSnils.Substring(3, 3)}-{cleanSnils.Substring(6, 3)} {cleanSnils.Substring(9, 2)}";
    }
    
    public int PaymentPercentage
    {
        get
        {
            // over 8 years => 100%
            if (ExperienceYears >= 8) return 100;
                
            // 5-8 years => 80%
            if (ExperienceYears >= 5) return 80;
                
            // <5 => 60%
            return 60;
        }
    }
    
    public decimal PaymentMultiplier => PaymentPercentage / 100m;
}