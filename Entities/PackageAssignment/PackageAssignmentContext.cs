using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Entities
{
    public class PackageAssignmentContext : DbContext
    {
        public PackageAssignmentContext(DbContextOptions<PackageAssignmentContext> options)
            : base(options)
        {
        }

        public DbSet<PackageAssignmentEntity> PackageAssignments { get; set; } = null!;
    }
}