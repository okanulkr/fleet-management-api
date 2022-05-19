using FleetManagementApi.Entities;
using FleetManagementApi.Dto;

namespace FleetManagementApi.Handlers.Vehicle.Commands
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