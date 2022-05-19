using FleetManagementApi.Entities;

namespace FleetManagementApi.Handlers.PackageAssignment.Commands
{
    public class PackageAssignmentCreateCommandHandler
    {
        PackageContext _packageRepository;
        PackageAssignmentContext _packageAssignmentRepository;
        public PackageAssignmentCreateCommandHandler(PackageContext packageRepository, PackageAssignmentContext packageAssignmentRepository)
        {
            _packageRepository = packageRepository;
            _packageAssignmentRepository = packageAssignmentRepository;
        }

        public PackageAssignmentCreateResponse? Handle(PackageAssignmentCreateRequest packageAssignment)
        {
            // handle existence

            // Update package state as 'LoadedIntoBag'
            PackageEntity package = _packageRepository.Packages.Single(x => x.Barcode == packageAssignment.Barcode);
            package.State = State.LoadedIntoBag;

            // Increment weight of bag by package weight
            PackageEntity bag = _packageRepository.Packages.Single(x => x.Barcode == packageAssignment.BagBarcode);
            bag.Weight += package.Weight;

            _packageRepository.SaveChanges();

            // Add package to bag assignment
            PackageAssignmentEntity entity = new PackageAssignmentEntity()
            {
                Barcode = packageAssignment.Barcode,
                BagBarcode = packageAssignment.BagBarcode
            };
            _packageAssignmentRepository.Add(entity);
            _packageAssignmentRepository.SaveChanges();

            return new PackageAssignmentCreateResponse { CompositeId = entity.BagBarcode + entity.Barcode };
        }
    }
}