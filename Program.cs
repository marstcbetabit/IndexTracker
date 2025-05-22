using IndexTracker.Application.Services;
using IndexTracker.Domain.Repositories;
using IndexTracker.Infrastructure.Persistence;
using IndexTracker.Infrastructure.Repositories;
using IndexTracker.Infrastructure.Services;
using IndexTracker.Application.Background;
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
                    services.AddScoped<ISp500Service, Infrastructure.Services.Sp500Service>();
                    services.AddHostedService<Application.Background.IndexValueBackgroundService>();
                });

            var app = builder.Build();
            await app.RunAsync();
        }
    }
}
