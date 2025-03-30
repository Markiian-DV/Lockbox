using Lockbox.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lockbox.Web.Infrastructure;

public static class MigrationsHelper
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using ApplicationDbContext dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        dbContext.Database.Migrate();

    }
}

