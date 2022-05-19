using FleetManagementApi.Entities;

public interface IPackageAssignmentRepository
{
    public string? Add(PackageAssignmentEntity packageAssignment);
    public PackageAssignmentEntity? GetByCompositeId(string compositeId);
}