using Microsoft.EntityFrameworkCore;
using PersonalPage.Application.Data;

namespace PersonalPage.API.ServiceRegistration
{
    public static class ServiceRegistration
    {
        // Extension method to configure Entity Framework Core DbContext
        public static IServiceCollection ConfigureEntityFrameWork(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<personalPageDbContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        // Extension method to initialize the database by applying migrations asynchronously
        public static async void InitializedDatabase(this WebApplication app)
        {
            // Create a scope to resolve services
            using var scope = app.Services.CreateScope();
            // Resolve the DbContext from the service provider
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<personalPageDbContext>();
            // Apply any pending migrations asynchronously
            await applicationDbContext.Database.MigrateAsync();
        }
    }
}

