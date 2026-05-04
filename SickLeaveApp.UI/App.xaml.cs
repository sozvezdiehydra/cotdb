using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SickLeaveApp.Application.Services;
using SickLeaveApp.Infrastructure.Data;
using SickLeaveApp.Infrastructure.Data.Repositories;
using SickLeaveApp.UI.ViewModels;

namespace SickLeaveApp.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            bool isPostgresAvailable = false;
            
            string postgresConn = "Host=localhost;Port=5432;Database=SickLeaveDb;Username=postgres;Password=postgres12;Timeout=2;CommandTimeout=2";
            
            try 
            {
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                optionsBuilder.UseNpgsql(postgresConn);
                
                using (var testContext = new AppDbContext(optionsBuilder.Options))
                {
                    if (testContext.Database.CanConnect())
                    {
                        isPostgresAvailable = true;
                    }
                }
            }
            catch
            {
                isPostgresAvailable = false;
            }
            
            if (!isPostgresAvailable)
            {
                optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlite("Data Source=sickleave.db");
            }
            
            var dbContext = new AppDbContext(optionsBuilder.Options);
            
            dbContext.Database.EnsureCreated();
            
            var calculatorService = new SickLeaveCalculatorService();
            var employeeRepository = new EmployeeRepository(dbContext);
            var mainWindowViewModel = new MainViewModel(calculatorService, employeeRepository, dbContext);
            
            string dbType = isPostgresAvailable ? "PostgreSQL" : "SQLite (Локально)";
            var mainWindow = new MainWindow { DataContext = mainWindowViewModel };
            mainWindow.Title += $" - Работает на {dbType}";
            
            mainWindow.Show();
        }
}

