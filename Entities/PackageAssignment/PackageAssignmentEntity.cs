namespace FleetManagementApi.Entities.PackageAssignment;

public class PackageAssignmentEntity
{
    public Guid Id { get; set; }
    public string? Barcode { get; set; }
    public string? BagBarcode { get; set; }
}
