using FleetManagementApi.Entities;
using FleetManagementApi.Dto;

namespace FleetManagementApi.Handlers.Vehicle.Query
{
    public class VehicleGetByIdQueryHandler
    {
        private readonly VehicleContext _repository;
        public VehicleGetByIdQueryHandler(VehicleContext repository)
        {
            _repository = repository;
        }

        public VehicleItemDto? GetById(string licensePlate)
        {
            VehicleEntity? entity = _repository.Vehicles.SingleOrDefault(x => x.LicensePlate == licensePlate);
            return VehicleItemDto.MapFrom(entity);
        }
    }
}