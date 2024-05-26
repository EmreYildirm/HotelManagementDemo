using HotelManagement.Api.Data;
using HotelManagement.Api.Features.Mapper;
using HotelManagement.Api.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Api.Features.Hotel;

public class GetHotelContactInformationDetail
{
    public record Request : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string AuthorizedName { get; set; }
        public string AuthorizedSurname { get; set; }
        public string CompanyTitle { get; set; }
        public List<ContactInformation> ContactInformations { get; set; }
    }

    public class ContactInformation
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string ContactInfoName { get; set; }
        public ContactInfoType ContactInfoType { get; set; }
        public string Info { get; set; }
    }

    public class Handler(ApplicationDbContext appDbContext) : IRequestHandler<Request, Response>
    {
        private readonly ApplicationDbContext _appDbContext = appDbContext;

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var responseModel = new Response();

            var hotels = _appDbContext.Hotels.Include(hotel => hotel.ContanInformation).FirstOrDefault(hotel => hotel.Id == request.Id);
            var response = hotels!.HotelToHotelDetailResponse();
            return response;
        }
    }
}