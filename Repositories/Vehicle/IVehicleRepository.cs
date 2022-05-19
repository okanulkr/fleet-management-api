using FleetManagementApi.Entities.Vehicle;

namespace FleetManagementApi.Repositories.Vehicle;

public interface IVehicleRepository
{
    public string Add(VehicleEntity vehicle);
    public VehicleEntity? GetByPlate(string licensePlate);
}