using FleetManagementApi.Entities;
using FleetManagementApi.Dto;

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