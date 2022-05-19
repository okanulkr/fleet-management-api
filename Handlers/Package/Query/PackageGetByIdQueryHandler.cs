using FleetManagementApi.Entities;
using FleetManagementApi.Dto;

namespace FleetManagementApi.Handlers.Package.Query
{
    public class PackageGetByIdQueryHandler
    {
        PackageContext _repository;

        public PackageGetByIdQueryHandler(PackageContext repository)
        {
            _repository = repository;
        }

        public PackageItemDto? Handle(string barcode)
        {
            PackageEntity? entity = _repository.Packages
                .SingleOrDefault(x => x.Barcode == barcode);
            return PackageItemDto.MapFrom(entity);
        }
    }
}