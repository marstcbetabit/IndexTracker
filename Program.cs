using IndexTracker.Application.Background;
using IndexTracker.Application.Services;
using IndexTracker.Domain.Repositories;
using IndexTracker.Infrastructure.Persistence;
using IndexTracker.Infrastructure.Repositories;
using IndexTracker.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IndexTracker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIndexTrackerServices(this IServiceCollection services)
        {
            services.AddDbContext<IndexDbContext>(options =>
                options.UseSqlite("Data Source=indextracker.db"));
            services.AddScoped<IIndexValueRepository, IndexValueRepository>();
            services.AddScoped<ISp500Service, Sp500Service>();
            services.AddHostedService<IndexValueBackgroundService>();
            return services;
        }
    }

    public static class HostExtensions
    {
        public static void EnsureDatabaseMigrated(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IndexDbContext>();
            db.Database.Migrate();
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddIndexTrackerServices();
                });

            var app = builder.Build();

            // Ensure database and migrations are applied
            app.EnsureDatabaseMigrated();

            await app.RunAsync();
        }
    }
}
