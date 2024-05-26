using HotelManagement.Api.Features.Mapper;
using HotelManagement.Api.Infrastructure.Database;
using MediatR;

namespace HotelManagement.Api.Features.Hotel;

public class ListAuthorizedPersons
{
    public record Request : IRequest<List<Response>>
    {
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string AuthorizedName { get; set; }
        public string AuthorizedSurname { get; set; }
        public string CompanyTitle { get; set; }
    }

    public class Handler(ApplicationDbContext appDbContext) : IRequestHandler<Request, List<Response>>
    {
        private readonly ApplicationDbContext _appDbContext = appDbContext;

        public async Task<List<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var responseModel = new List<Response>();

            var hotels = _appDbContext.Hotels.ToList();
            var responseList = hotels.ToIndexResponseList();
            responseModel = responseList;
            return responseModel;
        }
    }
}