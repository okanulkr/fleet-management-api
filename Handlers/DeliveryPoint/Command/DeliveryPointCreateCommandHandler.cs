using FleetManagementApi.Entities;

namespace FleetManagementApi.Handlers.DeliveryPoint.Commands
{
    public class DeliveryPointCreateCommandHandler
    {
        private readonly IDeliveryPointRepository _repository;
        public DeliveryPointCreateCommandHandler(IDeliveryPointRepository repository)
        {
            _repository = repository;
        }

        public DeliveryPointCreateResponse Handle(DeliveryPointCreateRequest deliveryPoint)
        {
            var entity = new DeliveryPointEntity() { Name = deliveryPoint.Name, Value = deliveryPoint.Value };
            return new DeliveryPointCreateResponse { Value = _repository.Add(entity) };
        }
    }
}