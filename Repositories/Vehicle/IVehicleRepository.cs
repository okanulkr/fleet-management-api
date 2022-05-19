using FleetManagementApi.Entities;

public interface IVehicleRepository
{
    public string Add(VehicleEntity vehicle);
    public VehicleEntity? GetByPlate(string licensePlate);
}