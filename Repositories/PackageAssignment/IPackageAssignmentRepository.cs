using FleetManagementApi.Entities.PackageAssignment;

namespace FleetManagementApi.Repositories.PackageAssignment;

public interface IPackageAssignmentRepository
{
    public string? Add(PackageAssignmentEntity packageAssignment);
    public PackageAssignmentEntity? GetByCompositeId(string compositeId);
    public PackageAssignmentEntity? GetBagByBarcode(string barcode);
    public IEnumerable<PackageAssignmentEntity> GetPackagesInsideBag(string barcode);
    public bool PackageInBag(string barcode);

}