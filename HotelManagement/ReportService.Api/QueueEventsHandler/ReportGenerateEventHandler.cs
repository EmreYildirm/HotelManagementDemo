using ReportService.Api.Data;
using ReportService.Api.Domain;
using ReportService.Api.Infrastructure.Database;
using ReportService.Api.QueueEvents;

namespace ReportService.Api.QueueEventsHandler;

public class ReportGenerateEventHandler
{
    private readonly ApplicationDbContext _context;

    public ReportGenerateEventHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handler(ReportGenerateEvent @event)
    {
        string reportId = @event.Report;
        var report = _context.Reports.First(x => x.Id == Guid.Parse(reportId));
        //httplient ile bilgileri alma

        _context.Reports.Update(report);
        Console.WriteLine("Sıra çalıştı.");
    }
}