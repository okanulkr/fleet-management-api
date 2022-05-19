using FleetManagementApi.Entities.Package;
using FleetManagementApi.Dto.Package;
using FleetManagementApi.Repositories.Package;

namespace FleetManagementApi.Handlers.Package.Commands
{
    public class BagCreateCommandHandler
    {
        IPackageRepository _repository;
        public BagCreateCommandHandler(IPackageRepository repository)
        {
            _repository = repository;
        }

        public BagCreateResponse? Handle(BagCreateRequest bag)
        {
            // handle existence
            PackageEntity entity = new PackageEntity();
            entity.PackageType = PackageType.Bag;
            entity.State = State.Created;
            entity.Barcode = bag.Barcode;
            entity.DeliveryPoint = bag.DeliveryPoint;
            return new BagCreateResponse { Barcode = _repository.Add(entity) };
        }
    }
}