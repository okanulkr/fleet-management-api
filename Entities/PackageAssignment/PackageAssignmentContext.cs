using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Entities.PackageAssignment
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