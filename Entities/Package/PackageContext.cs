using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Entities
{
    public class PackageContext : DbContext
    {
        public PackageContext(DbContextOptions<PackageContext> options)
            : base(options)
        {
        }

        public DbSet<PackageEntity> Packages { get; set; } = null!;
    }
}