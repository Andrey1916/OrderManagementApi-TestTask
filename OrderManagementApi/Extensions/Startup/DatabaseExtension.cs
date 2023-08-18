using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Infrastructure.Database;

namespace OrderManagementApi.Extensions.Startup;

public static class DatabaseExtension
{
    public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<DbContext, DataContext>(options =>
        {
            options.UseSqlServer(connectionString);

            if (!builder.Environment.IsProduction())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });

        return builder;
    }

    public static WebApplication SetupDatabase(this WebApplication app)
    {
        var scopeService = app.Services.GetService<IServiceScopeFactory>();
        if (scopeService is not null)
        {
            using var scope = scopeService.CreateScope();
            scope.ServiceProvider.GetRequiredService<DbContext>().Database.Migrate();
        }

        return app;
    }
}
