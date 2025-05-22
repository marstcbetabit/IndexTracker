using IndexTracker.Application.Models;

namespace IndexTracker.Application.Services
{
    public interface ISp500Service
    {
        Task<Sp500ValueDto> GetCurrentValueAsync(CancellationToken cancellationToken = default);
    }
}
