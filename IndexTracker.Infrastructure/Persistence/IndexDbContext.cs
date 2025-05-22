using IndexTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IndexTracker.Infrastructure.Persistence
{
    public class IndexDbContext : DbContext
    {
        public DbSet<IndexValue> IndexValues { get; set; }

        public IndexDbContext(DbContextOptions<IndexDbContext> options) : base(options) { }
    }
}
