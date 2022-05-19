using FleetManagementApi.Entities;
using FleetManagementApi.Dto;

namespace FleetManagementApi.Handlers.PackageAssignment.Query
{
    public class PackageAssignmentGetByIdQueryHandler
    {
        PackageAssignmentContext _repository;

        public PackageAssignmentGetByIdQueryHandler(PackageAssignmentContext repository)
        {
            _repository = repository;
        }

        public PackageAssignmentItemDto? Handle(string id)
        {
            PackageAssignmentEntity? entity = _repository.PackageAssignments
                .SingleOrDefault(x => x.BagBarcode + x.Barcode == id);
            return PackageAssignmentItemDto.MapFrom(entity);
        }
    }
}