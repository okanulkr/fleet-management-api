using FleetManagementApi.Entities;

namespace FleetManagementApi.Handlers.DeliveryPoint.Commands
{
    public class DeliveryPointCreateCommandHandler
    {
        private readonly DeliveryPointContext _repository;
        public DeliveryPointCreateCommandHandler(DeliveryPointContext repository)
        {
            _repository = repository;
        }

        public DeliveryPointCreateResponse CreateDeliveryPoint(DeliveryPointCreateRequest deliveryPoint)
        {
            var entity = new DeliveryPointEntity() { Name = deliveryPoint.Name, Value = deliveryPoint.Value };
            _repository.Add(entity);
            _repository.SaveChanges();
            return new DeliveryPointCreateResponse { Value = entity.Value };
        }
    }
}