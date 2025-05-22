using IndexTracker.Application.Background;
using IndexTracker.Application.Services;
using IndexTracker.Domain.Repositories;
using IndexTracker.Infrastructure;
using IndexTracker.Infrastructure.Persistence;
using IndexTracker.Infrastructure.Repositories;
using IndexTracker.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IndexTracker
{
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
