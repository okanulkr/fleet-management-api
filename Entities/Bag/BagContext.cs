using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Entities
{
    public class BagContext : DbContext
    {
        public BagContext(DbContextOptions<BagContext> options)
            : base(options)
        {
        }

        public DbSet<BagEntity> Bags { get; set; } = null!;
    }
}