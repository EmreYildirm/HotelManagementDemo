using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Api.QueueEvents;
using ReportService.Api.QueueEventsHandler;

namespace ReportService.Api.QueueConsumers;

public class ReportGenerateEventConsumer : BackgroundService
{
    private readonly IConnection _rabbitMqConnection;
    private readonly IServiceProvider _serviceProvider;

    public ReportGenerateEventConsumer(IConnection rabbitMqConnection, IServiceProvider serviceProvider)
    {
        _rabbitMqConnection = rabbitMqConnection;
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = _rabbitMqConnection.CreateModel();

        channel.QueueDeclare(
            queue: "report_generate_queue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var reportGenerateEvent = JsonSerializer.Deserialize<ReportGenerateEvent>(message);

            using (var scope = _serviceProvider.CreateScope())
            {
                var handler = scope.ServiceProvider.GetRequiredService<ReportGenerateEventHandler>();
                await handler.Handler(reportGenerateEvent);
            }
        };

        channel.BasicConsume(
            queue: "report_generate_queue",
            autoAck: true,
            consumer: consumer
        );

        return Task.CompletedTask;
    }
}