using FleetManagementApi.Entities;

namespace FleetManagementApi.Handlers.Package.Commands
{
    public class PackageCreateCommandHandler
    {
        PackageContext _repository;
        public PackageCreateCommandHandler(PackageContext repository)
        {
            _repository = repository;
        }

        public PackageCreateResponse? Handle(PackageCreateRequest package)
        {
            PackageEntity entity = new PackageEntity();
            entity.PackageType = PackageType.Package;
            entity.State = State.Created;
            entity.Barcode = package.Barcode;
            entity.DeliveryPoint = package.DeliveryPoint;
            entity.Weight = package.Weight;
            _repository.Add<PackageEntity>(entity);
            _repository.SaveChanges();
            return new PackageCreateResponse { Barcode = entity.Barcode };
        }
    }
}