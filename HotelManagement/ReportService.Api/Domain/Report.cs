using System.ComponentModel.DataAnnotations;
using ReportService.Api.Data;

namespace ReportService.Api.Domain;

public class Report
{
    [Key] public Guid Id { get; set; }
    public DateTime ReportDemandDate { get; set; }
    public ReportState State { get; set; }
    public virtual ICollection<ReportContent> ReportContents { get; set; }
}