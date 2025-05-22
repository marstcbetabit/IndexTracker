using Indextracker2.Application.Services;
using Indextracker2.Domain.Entities;
using Indextracker2.Domain.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Indextracker2.Application.Background
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
                        Console.WriteLine($"[FETCH/UPDATE] S&P 500: {value.Value} at {value.Timestamp:O}");
                        Console.Out.Flush();
                        _logger.LogInformation($"Fetched and updated S&P 500 value: {value.Value} at {value.Timestamp}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error fetching/updating S&P 500 value");
                    }
                    nextFetch = now.Add(fetchInterval);
                }
                try
                {
                    var latest = await _repository.GetLatestAsync(stoppingToken);
                    if (latest != null)
                    {
                        Console.WriteLine($"S&P 500: {latest.Value} at {latest.Timestamp:O}");
                        Console.Out.Flush();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error printing S&P 500 value");
                }
                await Task.Delay(printInterval, stoppingToken);
            }
        }
    }
}
