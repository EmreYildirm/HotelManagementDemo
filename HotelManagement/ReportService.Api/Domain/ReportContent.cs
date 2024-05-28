using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportService.Api.Domain;

public class ReportContent
{
    [Key] public Guid Id { get; set; }
    public string? Location { get; set; }
    public int? HotelCount { get; set; }
    public int? PhoneNumberCount { get; set; }
    [ForeignKey("Report")] public Guid ReportId { get; set; }
    public Report Report { get; set; }
}