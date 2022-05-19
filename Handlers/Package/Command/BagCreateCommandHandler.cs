using FleetManagementApi.Entities;

namespace FleetManagementApi.Handlers.Package.Commands
{
    public class BagCreateCommandHandler
    {
        PackageContext _repository;
        public BagCreateCommandHandler(PackageContext repository)
        {
            _repository = repository;
        }

        public BagCreateResponse? Handle(BagCreateRequest bag)
        {
            PackageEntity entity = new PackageEntity();
            entity.PackageType = PackageType.Bag;
            entity.State = State.Created;
            entity.Barcode = bag.Barcode;
            entity.DeliveryPoint = bag.DeliveryPoint;
            _repository.Add<PackageEntity>(entity);
            _repository.SaveChanges();
            return new BagCreateResponse { Barcode = entity.Barcode };
        }
    }
}