using FleetManagementApi.Entities.Vehicle;

namespace FleetManagementApi.Repositories.Vehicle;

public class VehicleRepository : IVehicleRepository
{
    private readonly VehicleContext _context;

    public VehicleRepository(VehicleContext context) { _context = context; }

    public string Add(VehicleEntity vehicle)
    {
        _context.Add(vehicle);
        _context.SaveChanges();
        return vehicle.LicensePlate!;
    }

    public VehicleEntity? GetByPlate(string licensePlate)
    {
        return _context.Vehicles.SingleOrDefault(x => x.LicensePlate == licensePlate);
    }
}