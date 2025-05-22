using IndexTracker.Application.Background;
using IndexTracker.Application.Services;
using IndexTracker.Domain.Repositories;
using IndexTracker.Infrastructure.Persistence;
using IndexTracker.Infrastructure.Repositories;
using IndexTracker.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IndexTracker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<IndexDbContext>(options =>
                        options.UseSqlite("Data Source=indextracker.db"));
                    services.AddScoped<IIndexValueRepository, IndexValueRepository>();
                    services.AddScoped<ISp500Service, Sp500Service>();
                    services.AddHostedService<IndexValueBackgroundService>();
                });

            var app = builder.Build();

            // Ensure database and migrations are applied
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IndexDbContext>();
                db.Database.Migrate();
            }

            await app.RunAsync();
        }
    }
}
