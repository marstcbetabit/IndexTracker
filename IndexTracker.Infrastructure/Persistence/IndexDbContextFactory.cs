using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IndexTracker.Infrastructure.Persistence
{
    public class IndexDbContextFactory : IDesignTimeDbContextFactory<IndexDbContext>
    {
        public IndexDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IndexDbContext>();
            // Use the same connection string as your app, or a default for migrations
            optionsBuilder.UseSqlite("Data Source=indextracker.db");
            return new IndexDbContext(optionsBuilder.Options);
        }
    }
}
