using FleetManagementApi.Entities.Package;
using FleetManagementApi.Dto.Package;
using FleetManagementApi.Repositories.Package;

namespace FleetManagementApi.Handlers.Package.Commands
{
    public class PackageCreateCommandHandler
    {
        private readonly IPackageRepository _repository;
        public PackageCreateCommandHandler(IPackageRepository repository)
        {
            _repository = repository;
        }

        public PackageCreateResponse? Handle(PackageCreateRequest package)
        {
            // handle existence
            PackageEntity entity = new PackageEntity();
            entity.PackageType = PackageType.Package;
            entity.State = State.Created;
            entity.Barcode = package.Barcode;
            entity.DeliveryPoint = package.DeliveryPoint;
            entity.Weight = package.Weight;
            return new PackageCreateResponse { Barcode = _repository.Add(entity) };
        }
    }
}