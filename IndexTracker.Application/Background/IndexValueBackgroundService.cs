using IndexTracker.Application.Services;
using IndexTracker.Domain.Entities;
using IndexTracker.Domain.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IndexTracker.Application.Background
{
    public class IndexValueBackgroundService : BackgroundService
    {
        private readonly ISp500Service _sp500Service;
        private readonly IIndexValueRepository _repository;
        private readonly ILogger<IndexValueBackgroundService> _logger;

        public IndexValueBackgroundService(ISp500Service sp500Service, IIndexValueRepository repository, ILogger<IndexValueBackgroundService> logger)
        {
            _sp500Service = sp500Service;
            _repository = repository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var fetchInterval = TimeSpan.FromSeconds(10);
            var printInterval = TimeSpan.FromSeconds(2);
            var nextFetch = DateTime.UtcNow;
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                if (now >= nextFetch)
                {
                    try
                    {
                        var value = await _sp500Service.GetCurrentValueAsync(stoppingToken);
                        await _repository.AddAsync(new IndexValue { Timestamp = value.Timestamp, Value = value.Value }, stoppingToken);
                        Console.WriteLine($"\n[{now:HH:mm:ss}] [HTTP] Updated S&P 500 value from Yahoo Finance: {value.Value:F2} (as of {value.Timestamp:yyyy-MM-dd HH:mm:ss})");
                        Console.WriteLine(new string('-', 60));
                        Console.Out.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[{now:HH:mm:ss}] [HTTP] Error updating S&P 500 value: {ex.Message}");
                        _logger.LogError(ex, "Error fetching/updating S&P 500 value");
                    }
                    nextFetch = now.Add(fetchInterval);
                }
                try
                {
                    var latest = await _repository.GetLatestAsync(stoppingToken);
                    if (latest != null)
                    {
                        Console.WriteLine($"[{now:HH:mm:ss}] [DB] Latest S&P 500 value: {latest.Value:F2} (as of {latest.Timestamp:yyyy-MM-dd HH:mm:ss})");
                        Console.Out.Flush();
                    }
                    else
                    {
                        Console.WriteLine($"[{now:HH:mm:ss}] [DB] No S&P 500 value in database.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{now:HH:mm:ss}] [DB] Error printing S&P 500 value: {ex.Message}");
                    _logger.LogError(ex, "Error printing S&P 500 value");
                }
                await Task.Delay(printInterval, stoppingToken);
            }
        }
    }
}
