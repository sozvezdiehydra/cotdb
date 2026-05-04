using System.IO;
using System.Xml.Serialization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SickLeaveApp.Application.DTOs;
using SickLeaveApp.Domain.Entities;

namespace SickLeaveApp.Infrastructure.Services
{
    public class SfrExportData
    {
        public string EmployeeName { get; set; }
        public string Snils { get; set; }
        public string Period { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SfrAmount { get; set; }
    }
    
    public class ExportService
    {
        public void ExportToPdf(string filePath, Employee emp, SickLeavePeriod sickLeave, CalculationsResult result)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily(Fonts.Verdana));

                    // ШАПКА
                    page.Header().Text("СПРАВКА-РАСЧЕТ ПОСОБИЯ")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content().PaddingVertical(20).Column(col =>
                    {
                        col.Spacing(10);
                        
                        col.Item().Text($"Сотрудник: {emp.FullName}").Bold();
                        col.Item().Text($"СНИЛС: {emp.Snils}");
                        col.Item().Text($"Стаж: {emp.ExperienceYears} лет ({emp.PaymentPercentage}%)");
                        
                        col.Item().PaddingTop(10).Text("Период нетрудоспособности:").Bold();
                        col.Item().Text($"{sickLeave.StartDate:dd.MM.yyyy} — {sickLeave.EndDate:dd.MM.yyyy} ({sickLeave.TotalDays} дн.)");
                        
                        col.Item().PaddingTop(20).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(120);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Показатель");
                                header.Cell().Element(CellStyle).Text("Сумма (руб.)");
                                
                                static IContainer CellStyle(IContainer container) => container.DefaultTextStyle(x => x.Bold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                            });

                            table.Cell().Text("Среднедневной заработок");
                            table.Cell().Text($"{result.AverageDailyEarnings:N2}");

                            table.Cell().Text("За счет работодателя (3 дня)");
                            table.Cell().Text($"{result.EmployerAmount:N2}");

                            table.Cell().Text("За счет СФР");
                            table.Cell().Text($"{result.SfrAmount:N2}");

                            table.Cell().Background(Colors.Grey.Lighten3).Text("ИТОГО К ВЫПЛАТЕ").Bold();
                            table.Cell().Background(Colors.Grey.Lighten3).Text($"{result.TotalAmount:N2}").Bold();
                        });
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Сформировано в приложении 'Мой Больничный' - ");
                        x.CurrentPageNumber();
                    });
                });
            }).GeneratePdf(filePath);
        }
        
        public void ExportToXml(string filePath, Employee emp, SickLeavePeriod sickLeave, CalculationsResult result)
        {
            var data = new SfrExportData
            {
                EmployeeName = emp.FullName,
                Snils = emp.Snils,
                Period = $"{sickLeave.StartDate:yyyy-MM-dd} до {sickLeave.EndDate:yyyy-MM-dd}",
                TotalAmount = result.TotalAmount,
                SfrAmount = result.SfrAmount
            };
            
            var serializer = new XmlSerializer(typeof(SfrExportData));
        
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}