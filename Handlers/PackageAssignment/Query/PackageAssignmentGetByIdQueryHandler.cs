using FleetManagementApi.Entities.PackageAssignment;
using FleetManagementApi.Dto.PackageAssignment;
using FleetManagementApi.Repositories.PackageAssignment;

namespace FleetManagementApi.Handlers.PackageAssignment.Query
{
    public class PackageAssignmentGetByIdQueryHandler
    {
        private readonly IPackageAssignmentRepository _repository;

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