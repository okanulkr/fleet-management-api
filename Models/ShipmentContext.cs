using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Models
{
    public class ShipmentContext : DbContext
    {
        public ShipmentContext(DbContextOptions<ShipmentContext> options)
            : base(options)
        {
        }

        public DbSet<ShipmentEntity> Shipments { get; set; } = null!;
    }
}