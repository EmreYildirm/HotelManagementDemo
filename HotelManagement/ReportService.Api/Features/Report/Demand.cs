using System.Text;
using System.Text.Json;
using MediatR;
using RabbitMQ.Client;
using ReportService.Api.Data;
using ReportService.Api.Infrastructure.Database;
using ReportService.Api.QueueEvents;

namespace ReportService.Api.Features.Report;

public class Demand
{
    public record Request : IRequest<Response>
    {
    }

    public class Response
    {
        public Guid Id { get; set; }
    }

    public class Handler(IConnection rabbitMqConnection, ApplicationDbContext applicationDbContext) : IRequestHandler<Request, Response>
    {
        private readonly IConnection _rabbitMqConnection = rabbitMqConnection;
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            //Rapor oluşturulma sıraya da alınabilir.
            Domain.Report report = new Domain.Report()
            {
                State = ReportState.InProgress,
                ReportDemandDate = DateTime.UtcNow
            };
            _applicationDbContext.Reports.Add(report);
            await _applicationDbContext.SaveChangesAsync();

            var reportGenerateEvent = new ReportGenerateEvent(report.Id.ToString());
            PublishEvent(reportGenerateEvent);
            return new Response()
            {
                Id = report.Id
            };
        }

        private void PublishEvent(ReportGenerateEvent @event)
        {
            using (var channel = _rabbitMqConnection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "report_generate_queue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var message = JsonSerializer.Serialize(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "report_generate_queue",
                    basicProperties: null,
                    body: body
                );
            }
        }
    }
}