using Lockbox.Application.Contracts;
using Lockbox.Infrastructure.Data;
using Lockbox.Infrastructure.EmailServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Lockbox.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PSQL");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, "Connection string is missing");

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddTransient<IMailService, LocalEmailSender>();
        
        return services;
    }
}