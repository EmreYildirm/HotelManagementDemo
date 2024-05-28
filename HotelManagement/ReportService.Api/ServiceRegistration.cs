using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using ReportService.Api.Infrastructure.Database;
using ReportService.Api.QueueConsumers;
using ReportService.Api.QueueEventsHandler;

namespace ReportService.Api;

public static class ServiceRegistration
{
    public static IServiceCollection AddReportApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddHttpContextAccessor();

        services.AddDbContext<ApplicationDbContext>(
            options => { options.UseNpgsql(configuration.GetConnectionString("DbConnection"), m => { m.EnableRetryOnFailure(); }); });


        services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));

        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        services.AddSingleton(connection);
        // EventHandler kayıtları
        services.AddScoped<ReportGenerateEventHandler>();

        // Consumer Service kaydı
        services.AddHostedService<ReportGenerateEventConsumer>();
        return services;
    }

    public static void UseService(this IApplicationBuilder app)
    {
        var serviceProvider = app.ApplicationServices;
        using var serviceScope = serviceProvider.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context?.Database.Migrate();
    }
}