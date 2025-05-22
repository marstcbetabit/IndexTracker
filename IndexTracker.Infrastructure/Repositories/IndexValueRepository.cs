using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IndexTracker.Domain.Entities;
using IndexTracker.Domain.Repositories;
using IndexTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IndexTracker.Infrastructure.Repositories
{
    public class IndexValueRepository : IIndexValueRepository
    {
        private readonly IndexDbContext _context;
        public IndexValueRepository(IndexDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(IndexValue value, CancellationToken cancellationToken = default)
        {
            _context.IndexValues.Add(value);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IndexValue?> GetLatestAsync(CancellationToken cancellationToken = default)
        {
            return await _context.IndexValues.OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<IndexValue>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.IndexValues.OrderByDescending(x => x.Timestamp).ToListAsync(cancellationToken);
        }
    }
}
