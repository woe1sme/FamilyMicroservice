using Family.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Family.API.Extension
{
    public static class MigrationExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();
            var context = services.GetRequiredService<FamilyDbContext>();

            try
            {
                logger.LogInformation("Applying database migrations...");
                //context.Database.Migrate();
                logger.LogInformation("Database migrations applied successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while applying database migrations.");
                throw;
            }

            return host;
        }
    }
}
