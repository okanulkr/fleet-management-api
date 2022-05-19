using FleetManagementApi.Entities;
using FleetManagementApi.Dto;

namespace FleetManagementApi.Handlers.Vehicle.Query
{
    public class VehicleGetByIdQueryHandler
    {
        private readonly IVehicleRepository _repository;
        public VehicleGetByIdQueryHandler(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public VehicleItemDto? Handle(string licensePlate)
        {
            VehicleEntity? entity = _repository.GetByPlate(licensePlate);
            return VehicleItemDto.MapFrom(entity);
        }
    }
}