using HotelManagement.Api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Api;

public static class ServiceRegistration
{
    public static IServiceCollection AddHotelApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddHttpContextAccessor();

        services.AddDbContext<ApplicationDbContext>(
            options => { options.UseNpgsql(configuration.GetConnectionString("DbConnection"), m => { m.EnableRetryOnFailure(); }); });

        services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));
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