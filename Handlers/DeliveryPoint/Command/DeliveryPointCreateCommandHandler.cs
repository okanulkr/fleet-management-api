using FleetManagementApi.Entities.DeliveryPoint;
using FleetManagementApi.Dto.DeliveryPoint;
using FleetManagementApi.Repositories.DeliveryPoint;

namespace FleetManagementApi.Handlers.DeliveryPoint.Commands
{
    public class DeliveryPointCreateCommandHandler
    {
        private readonly IDeliveryPointRepository _repository;
        public DeliveryPointCreateCommandHandler(IDeliveryPointRepository repository)
        {
            _repository = repository;
        }

        public DeliveryPointCreateResponse? Handle(DeliveryPointCreateRequest deliveryPoint)
        {
            // handle existence
            var entity = new DeliveryPointEntity() { Name = deliveryPoint.Name, Value = deliveryPoint.Value };
            return new DeliveryPointCreateResponse { Value = _repository.Add(entity) };
        }
    }
}