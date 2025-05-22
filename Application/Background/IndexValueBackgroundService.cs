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
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var value = await _sp500Service.GetCurrentValueAsync(stoppingToken);
                    await _repository.AddAsync(new IndexValue { Timestamp = value.Timestamp, Value = value.Value }, stoppingToken);
                    _logger.LogInformation($"Saved S&P 500 value: {value.Value} at {value.Timestamp}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving S&P 500 value");
                }
                await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
            }
        }
    }
}
