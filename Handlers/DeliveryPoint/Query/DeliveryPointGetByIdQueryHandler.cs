using FleetManagementApi.Entities;
using FleetManagementApi.Dto;

namespace FleetManagementApi.Handlers.DeliveryPoint.Query
{
    public class DeliveryPointGetByIdQueryHandler
    {
        private readonly DeliveryPointContext _repository;
        public DeliveryPointGetByIdQueryHandler(DeliveryPointContext repository)
        {
            _repository = repository;
        }

        public DeliveryPointItemDto? GetById(int value)
        {
            DeliveryPointEntity? entity = _repository.DeliveryPoints.SingleOrDefault(x => x.Value == value);
            return DeliveryPointItemDto.MapFrom(entity);
        }
    }
}