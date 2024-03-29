using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Entities.DeliveryPoint
{
    public class DeliveryPointContext : DbContext
    {
        public DeliveryPointContext(DbContextOptions<DeliveryPointContext> options)
            : base(options)
        {
        }

        public DbSet<DeliveryPointEntity> DeliveryPoints { get; set; } = null!;
    }
}