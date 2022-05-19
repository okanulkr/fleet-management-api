using FleetManagementApi.Entities;

public interface IDeliveryPointRepository
{
    public int Add(DeliveryPointEntity vehicle);
    public DeliveryPointEntity? GetByValue(int value);
}