using FleetManagementApi.Entities;
using FleetManagementApi.Dto;

namespace FleetManagementApi.Handlers.PackageAssignment.Query
{
    public class PackageAssignmentGetByIdQueryHandler
    {
        IPackageAssignmentRepository _repository;

        public PackageAssignmentGetByIdQueryHandler(IPackageAssignmentRepository repository)
        {
            _repository = repository;
        }

        public PackageAssignmentItemDto? Handle(string id)
        {
            PackageAssignmentEntity? entity = _repository.GetByCompositeId(id);
            return PackageAssignmentItemDto.MapFrom(entity);
        }
    }
}