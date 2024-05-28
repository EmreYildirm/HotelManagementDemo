namespace ReportService.Api.QueueEvents;

public class ReportGenerateEvent
{
    public string Report { get; }

    public ReportGenerateEvent(string report)
    {
        Report = report;
    }
}