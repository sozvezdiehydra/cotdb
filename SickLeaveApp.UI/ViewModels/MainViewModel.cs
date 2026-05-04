using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SickLeaveApp.Domain.Enums;
using SickLeaveApp.Application.DTOs;
using SickLeaveApp.Application.Services;
using SickLeaveApp.Domain.Entities;
using SickLeaveApp.Domain.Repositories;
using SickLeaveApp.Infrastructure.Data;

namespace SickLeaveApp.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly SickLeaveCalculatorService _calculator;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly AppDbContext _dbContext;
        
        public MainViewModel(SickLeaveCalculatorService calculator, IEmployeeRepository employeeRepository, AppDbContext dbContext)
        {
            _calculator = calculator;
            _employeeRepository = employeeRepository;
            _dbContext = dbContext;
            
            // Incomes.Add(new IncomeItem { Year = DateTime.Now.Year - 2, Amount = 0 });
            // Incomes.Add(new IncomeItem { Year = DateTime.Now.Year - 1, Amount = 0 });
        }

        [ObservableProperty] private string _fullName = "";
        [ObservableProperty] private string _snils = "";
        [ObservableProperty] private int _experienceYears = 0;

        [ObservableProperty] private DateTime _startDate = DateTime.Today.AddDays(-10);
        [ObservableProperty] private DateTime _endDate = DateTime.Today;
        [ObservableProperty] private SickLeaveReason _selectedReason = SickLeaveReason.Illness;

        public Array Reasons => Enum.GetValues(typeof(SickLeaveReason));

        public ObservableCollection<IncomeItem> Incomes { get; set; } = new();

        [ObservableProperty] private CalculationsResult _result;[RelayCommand]
        private void Calculate()
        {
            try
            {
                var employee = new Employee(_fullName, _snils, _experienceYears, 0);
                var sickLeave = new SickLeavePeriod(employee.Id, _startDate, _endDate, _selectedReason);
                
                var incomeRecords = Incomes.Select(i => new IncomeRecord(employee.Id, i.Year, i.Amount)).ToList();
                
                Result = _calculator.Calculate(employee, sickLeave, incomeRecords);
                
                _employeeRepository.Add(employee);
                _dbContext.SickLeavePeriods.Add(sickLeave);
                _dbContext.IncomeRecords.AddRange(incomeRecords);
                _dbContext.SaveChanges();

                MessageBox.Show("Расчет успешно выполнен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода, проверьте правильность введеных данных!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class IncomeItem
    {
        public int Year { get; set; }
        public decimal Amount { get; set; }
    }
}