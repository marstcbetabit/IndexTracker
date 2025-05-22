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
                });

            var app = builder.Build();

            await using (var scope = app.Services.CreateAsyncScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IndexDbContext>();
                await db.Database.EnsureCreatedAsync();

                var sp500Service = scope.ServiceProvider.GetRequiredService<ISp500Service>();
                while (true)
                {
                    try
                    {
                        var value = await sp500Service.GetCurrentValueAsync();
                        Console.WriteLine($"S&P 500: {value.Value} at {value.Timestamp:O}\n");
                        Console.Out.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }
            }
        }
    }
}
