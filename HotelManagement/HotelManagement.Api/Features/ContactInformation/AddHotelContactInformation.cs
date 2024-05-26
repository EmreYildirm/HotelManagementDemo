using HotelManagement.Api.Data;
using HotelManagement.Api.Features.Mapper;
using HotelManagement.Api.Infrastructure.Database;
using MediatR;

namespace HotelManagement.Api.Features.ContactInformation;

public class AddHotelContactInformation
{
    public record Request : IRequest<Response>
    {
        public Guid HotelId { get; set; }
        public ContactInfoType ContactInfoType { get; set; }
        public string InfoContent { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
    }

    public class Handler(ApplicationDbContext appDbContext) : IRequestHandler<Request, Response>
    {
        private readonly ApplicationDbContext _appDbContext = appDbContext;

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var responseModel = new Response();

            var entity = request.ContactInformationRequestToContactInformation();

            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            responseModel.Id = entity.Id;
            return responseModel;
        }
    }
}