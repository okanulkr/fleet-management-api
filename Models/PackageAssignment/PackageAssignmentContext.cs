using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Models
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