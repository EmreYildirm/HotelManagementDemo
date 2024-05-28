using MediatR;
using Microsoft.EntityFrameworkCore;
using ReportService.Api.Data;
using ReportService.Api.Infrastructure.Database;
using ReportService.Api.QueueEvents;

namespace ReportService.Api.Features.Report;

public class GetReports
{
    public record Request : IRequest<List<Response>>
    {
    }

    public class Response
    {
        public Guid Id { get; set; }
        public ReportState ReportState { get; set; }
        public string State { get; set; }
        public DateTime DemandDate { get; set; }
        public List<LocationReportModel> LocationReportModels { get; set; }
    }

    public class LocationReportModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public int HotelCount { get; set; }
        public int PhoneCount { get; set; }
    }

    public class Handler(ApplicationDbContext applicationDbContext) : IRequestHandler<Request, List<Response>>
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<List<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var response = new List<Response>();

            var reports = _applicationDbContext.Reports.Where(x => x.State == ReportState.Ready).Include(x => x.ReportContents).ToList();
            response = reports.ToReportResponseList();
            return response;
        }
    }
}