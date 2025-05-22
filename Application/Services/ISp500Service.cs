using Indextracker2.Application.Models;

namespace Indextracker2.Application.Services
{
    public interface ISp500Service
    {
        Task<Sp500ValueDto> GetCurrentValueAsync(CancellationToken cancellationToken = default);
    }
}
