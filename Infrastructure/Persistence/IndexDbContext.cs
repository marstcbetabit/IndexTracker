using Indextracker2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Indextracker2.Infrastructure.Persistence
{
    public class IndexDbContext : DbContext
    {
        public DbSet<IndexValue> IndexValues { get; set; }

        public IndexDbContext(DbContextOptions<IndexDbContext> options) : base(options) { }
    }
}
