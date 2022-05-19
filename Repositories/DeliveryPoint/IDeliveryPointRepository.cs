using FleetManagementApi.Entities.DeliveryPoint;

namespace FleetManagementApi.Repositories.DeliveryPoint;

public interface IDeliveryPointRepository
{
    public int Add(DeliveryPointEntity vehicle);
    public DeliveryPointEntity? GetByValue(int value);
}