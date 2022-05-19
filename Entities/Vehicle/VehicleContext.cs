using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Entities.Vehicle
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