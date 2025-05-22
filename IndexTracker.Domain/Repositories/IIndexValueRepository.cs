using IndexTracker.Domain.Entities;

namespace IndexTracker.Domain.Repositories
{
    public interface IIndexValueRepository
    {
        Task AddAsync(IndexValue value, CancellationToken cancellationToken = default);
        Task<IndexValue?> GetLatestAsync(CancellationToken cancellationToken = default);
        Task<List<IndexValue>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
