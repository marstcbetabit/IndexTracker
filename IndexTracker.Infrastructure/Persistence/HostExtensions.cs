using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IndexTracker.Infrastructure.Persistence
{
    public static class HostExtensions
    {
        public static void EnsureDatabaseMigrated(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IndexDbContext>();
            db.Database.Migrate();
        }
    }
}
