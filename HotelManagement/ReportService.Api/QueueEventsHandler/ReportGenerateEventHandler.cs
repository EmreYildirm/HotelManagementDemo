using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
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
        List<ReportContent> reportContents = new();
        string reportId = @event.Report;
        var report = _context.Reports.First(x => x.Id == Guid.Parse(reportId));
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync("https://localhost:7187/api/Integration/get-report-info");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var httpResponseMessageString = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ReportGenerateResponse>(httpResponseMessageString)!;
                foreach (var locatinReport in responseData.LocationReportModels)
                {
                    ReportContent reportContent = new ReportContent()
                    {
                        Location = locatinReport.Location,
                        HotelCount = locatinReport.HotelCount,
                        ReportId = Guid.Parse(reportId),
                        PhoneNumberCount = locatinReport.PhoneCount
                    };
                    reportContents.Add(reportContent);
                }

                _context.ReportContents.AddRange(reportContents);
            }
            else
            {
                Console.WriteLine($"HTTP error: {httpResponseMessage.StatusCode}");
            }
        }

        report.State = ReportState.Ready;
        _context.Reports.Update(report);
        await _context.SaveChangesAsync();
        Console.WriteLine("Sıra çalıştı.");
    }
}