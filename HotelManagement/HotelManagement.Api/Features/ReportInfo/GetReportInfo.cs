using HotelManagement.Api.Data;
using HotelManagement.Api.Features.Mapper;
using HotelManagement.Api.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Api.Features.ReportInfo;

public class GetReportInfo
{
    public record Request : IRequest<Response>
    {
    }

    public class Response
    {
        public List<LocationReportModel> LocationReportModels { get; set; }
    }

    public class LocationReportModel
    {
        public string Location { get; set; }
        public int HotelCount { get; set; }
        public int PhoneCount { get; set; }
    }

    public class Handler(ApplicationDbContext appDbContext) : IRequestHandler<Request, Response>
    {
        private readonly ApplicationDbContext _appDbContext = appDbContext;

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var responseModel = new Response();
            // Calculate the number of hotels and phone numbers in the given location

            var locationReports = await _appDbContext.ContactInformations
                .Where(ci => ci.InfoType == ContactInfoType.Location)
                .GroupBy(ci => ci.InfoContent)
                .Select(g => new LocationReportModel
                {
                    Location = g.Key,
                    HotelCount = g.SelectMany(ci => ci.Hotel.ContanInformation)
                        .Count(ci => ci.InfoType == ContactInfoType.Location && ci.InfoContent == g.Key),
                    PhoneCount = g.SelectMany(ci => ci.Hotel.ContanInformation)
                        .Count(ci => ci.InfoType == ContactInfoType.PhoneNumber && ci.InfoContent == g.Key)
                })
                .ToListAsync();
            responseModel.LocationReportModels = locationReports;

            return responseModel;
        }
    }
}