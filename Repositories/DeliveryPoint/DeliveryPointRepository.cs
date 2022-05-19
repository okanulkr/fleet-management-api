using FleetManagementApi.Entities;

public class DeliveryPointRepository : IDeliveryPointRepository
{
    private readonly DeliveryPointContext _context;

    public DeliveryPointRepository(DeliveryPointContext context) { _context = context; }

    public int Add(DeliveryPointEntity vehicle)
    {
        _context.Add(vehicle);
        _context.SaveChanges();
        return vehicle.Value;
    }

    public DeliveryPointEntity? GetByValue(int value)
    {
        return _context.DeliveryPoints.SingleOrDefault(x => x.Value == value);
    }
}