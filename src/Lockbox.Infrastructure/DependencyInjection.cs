using Lockbox.Application.Contracts;
using Lockbox.Infrastructure.Data;
using Lockbox.Infrastructure.EmailServices;
using Lockbox.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
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
            // TODO: add interceptor to handle CreatedOn, ModifiedOn etc...
            opt.UseNpgsql(connectionString);
        });

        // AddAuthentication
        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped<IMailService, LocalEmailSender>();
        
        return services;
    }
}