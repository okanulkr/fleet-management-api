using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Models
{
    public class VehicleContext : DbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleEntity> Vehicles { get; set; } = null!;
    }
}