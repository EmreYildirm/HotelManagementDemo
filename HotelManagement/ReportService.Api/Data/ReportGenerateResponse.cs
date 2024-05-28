namespace ReportService.Api.Data;

public class ReportGenerateResponse
{
    public List<LocationReportModel> LocationReportModels { get; set; }
}
public class LocationReportModel
{
    public string Location { get; set; }
    public int HotelCount { get; set; }
    public int PhoneCount { get; set; }
}