using FleetManagementApi.Entities.DeliveryPoint;
using FleetManagementApi.Dto.DeliveryPoint;
using FleetManagementApi.Repositories.DeliveryPoint;

namespace FleetManagementApi.Handlers.DeliveryPoint.Query
{
    public class DeliveryPointGetByIdQueryHandler
    {
        private readonly IDeliveryPointRepository _repository;
        public DeliveryPointGetByIdQueryHandler(IDeliveryPointRepository repository)
        {
            _repository = repository;
        }

        public DeliveryPointItemDto? Handle(int value)
        {
            DeliveryPointEntity? entity = _repository.GetByValue(value);
            return DeliveryPointItemDto.MapFrom(entity);
        }
    }
}