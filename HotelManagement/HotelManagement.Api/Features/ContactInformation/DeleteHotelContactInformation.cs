using HotelManagement.Api.Infrastructure.Database;
using MediatR;

namespace HotelManagement.Api.Features.ContactInformation;

public class DeleteHotelContactInformation
{
    public record Request : IRequest<Response>
    {
        public Guid Id { get; set; }
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

            var entityToDelete = _appDbContext.ContactInformations.FirstOrDefault(e => e.Id == request.Id);
            _appDbContext.Remove(entityToDelete);
            await _appDbContext.SaveChangesAsync();
            responseModel.Id = entityToDelete.Id;
            return responseModel;
        }
    }
}