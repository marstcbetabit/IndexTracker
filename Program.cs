using Indextracker2.Application.Services;
using Indextracker2.Domain.Repositories;
using Indextracker2.Infrastructure.Persistence;
using Indextracker2.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Indextracker2
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
