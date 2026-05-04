using Microsoft.EntityFrameworkCore;
using SickLeaveApp.Domain.Entities;

namespace SickLeaveApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SickLeavePeriod> SickLeavePeriods { get; set; }
    public DbSet<IncomeRecord> IncomeRecords { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated(); 
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<SickLeavePeriod>().ToTable("SickLeavePeriods");
        modelBuilder.Entity<IncomeRecord>().ToTable("IncomeRecords");
        
        modelBuilder.Entity<Employee>()
            .Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Snils)
            .IsRequired()
            .HasMaxLength(14); 
    }
}