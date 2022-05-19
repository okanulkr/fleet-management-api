using FleetManagementApi.Entities.Package;
using FleetManagementApi.Entities.PackageAssignment;
using FleetManagementApi.Dto.PackageAssignment;
using FleetManagementApi.Repositories.PackageAssignment;
using FleetManagementApi.Repositories.Package;

namespace FleetManagementApi.Handlers.PackageAssignment.Command
{
    public class PackageAssignmentCreateCommandHandler
    {
        IPackageRepository _packageRepository;
        IPackageAssignmentRepository _packageAssignmentRepository;
        public PackageAssignmentCreateCommandHandler(IPackageRepository packageRepository, IPackageAssignmentRepository packageAssignmentRepository)
        {
            _packageRepository = packageRepository;
            _packageAssignmentRepository = packageAssignmentRepository;
        }

        public PackageAssignmentCreateResponse? Handle(PackageAssignmentCreateRequest packageAssignment)
        {
            // handle existence

            // Update package state as 'LoadedIntoBag'
            PackageEntity? package = _packageRepository.GetByBarcode(packageAssignment.Barcode!);
            package!.State = State.LoadedIntoBag;

            // Increment weight of bag by package weight
            PackageEntity? bag = _packageRepository.GetByBarcode(packageAssignment.BagBarcode!);
            bag!.Weight += package.Weight;

            _packageRepository.SaveChanges();

            // Add package to bag assignment
            PackageAssignmentEntity entity = new PackageAssignmentEntity()
            {
                Barcode = packageAssignment.Barcode,
                BagBarcode = packageAssignment.BagBarcode
            };

            return new PackageAssignmentCreateResponse { CompositeId = _packageAssignmentRepository.Add(entity) };
        }
    }
}