using FleetManagementApi.Entities.Package;
using FleetManagementApi.Dto.Package;
using FleetManagementApi.Repositories.Package;

namespace FleetManagementApi.Handlers.Package.Query
{
    public class PackageGetByIdQueryHandler
    {
        IPackageRepository _repository;

        public PackageGetByIdQueryHandler(IPackageRepository repository)
        {
            _repository = repository;
        }

        public PackageItemDto? Handle(string barcode)
        {
            PackageEntity? entity = _repository.GetByBarcode(barcode);
            return PackageItemDto.MapFrom(entity);
        }
    }
}