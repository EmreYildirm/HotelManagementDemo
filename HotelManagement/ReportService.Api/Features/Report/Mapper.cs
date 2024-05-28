using ReportService.Api.Data;

namespace ReportService.Api.Features.Report;

public static class Mapper
{
    public static List<Features.Report.GetReports.Response> ToReportResponseList(this List<Domain.Report> reports)
    {
        return reports.Select(p => p.ToReportResponse()).ToList();
    }

    private static Features.Report.GetReports.Response ToReportResponse(this Domain.Report report)
    {
        var response = new GetReports.Response()
        {
            LocationReportModels = new()
        };
        string infoName = Enum.GetName(typeof(ReportState), report.State);
        response.Id = report.Id;
        response.State = infoName;
        response.ReportState = report.State;
        response.DemandDate = report.ReportDemandDate;
        response.LocationReportModels = report.ReportContents.ToList().ToResponseReportContentList();

        return response;
    }

    private static List<Features.Report.GetReports.LocationReportModel> ToResponseReportContentList(this List<Domain.ReportContent> reportContents)
    {
        return reportContents.Select(p => p.ToReportContentResponse()).ToList();
    }

    private static Features.Report.GetReports.LocationReportModel ToReportContentResponse(this Domain.ReportContent reportContent)
    {
        var response = new GetReports.LocationReportModel();
        response.Id = reportContent.Id;
        response.Location = reportContent.Location ?? "";
        response.HotelCount = reportContent.HotelCount ?? 0;
        response.PhoneCount = reportContent.PhoneNumberCount ?? 0;

        return response;
    }
}