using HotelManagement.Api.Features.Mapper;
using HotelManagement.Api.Infrastructure.Database;
using MediatR;

namespace HotelManagement.Api.Features.Hotel;

public class Create
{
    public record Request : IRequest<Response>
    {
        public string AuthorizedName { get; set; }
        public string AuthorizedSurname { get; set; }
        public string CompanyTitle { get; set; }
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

            var entity = request.CreateRequestToHotel();

            await _appDbContext.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            responseModel.Id = entity.Id;
            return responseModel;
        }
    }
}