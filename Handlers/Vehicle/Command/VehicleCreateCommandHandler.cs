using FleetManagementApi.Entities;
using FleetManagementApi.Dto;

namespace FleetManagementApi.Handlers.Vehicle.Commands
{
    public class VehicleCreateCommandHandler
    {
        private readonly VehicleContext _repository;
        public VehicleCreateCommandHandler(VehicleContext repository)
        {
            _repository = repository;
        }

        public VehicleCreateResponse CreateVehicle(VehicleCreateRequest vehicle)
        {
            var entity = new VehicleEntity() { LicensePlate = vehicle.LicensePlate };
            _repository.Add(entity);
            _repository.SaveChanges();
            return new VehicleCreateResponse { LicensePlate = entity.LicensePlate };
        }
    }
}