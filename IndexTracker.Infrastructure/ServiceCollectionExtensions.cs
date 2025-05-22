using IndexTracker.Application.Background;
using IndexTracker.Application.Services;
using IndexTracker.Domain.Repositories;
using IndexTracker.Infrastructure.Persistence;
using IndexTracker.Infrastructure.Repositories;
using IndexTracker.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IndexTracker.Infrastructure
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
}
