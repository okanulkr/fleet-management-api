using FleetManagementApi.Entities.Vehicle;
using FleetManagementApi.Dto.Vehicle;
using FleetManagementApi.Repositories.Vehicle;

namespace FleetManagementApi.Handlers.Vehicle.Command
{
    public class VehicleCreateCommandHandler
    {
        private readonly IVehicleRepository _repository;
        public VehicleCreateCommandHandler(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public VehicleCreateResponse? Handle(VehicleCreateRequest vehicle)
        {
            // handle existence
            var entity = new VehicleEntity() { LicensePlate = vehicle.LicensePlate };
            return new VehicleCreateResponse { LicensePlate = _repository.Add(entity) };
        }
    }
}