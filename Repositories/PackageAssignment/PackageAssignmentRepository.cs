using FleetManagementApi.Entities;

public class PackageAssignmentRepository : IPackageAssignmentRepository
{
    private readonly PackageAssignmentContext _context;

    public PackageAssignmentRepository(PackageAssignmentContext context) { _context = context; }

    public string? Add(PackageAssignmentEntity packageAssignment)
    {
        _context.Add(packageAssignment);
        _context.SaveChanges();
        return packageAssignment.BagBarcode + packageAssignment.Barcode;
    }

    public PackageAssignmentEntity? GetByCompositeId(string compositeId)
    {
        return _context.PackageAssignments.SingleOrDefault(x => x.BagBarcode + x.Barcode == compositeId);
    }

    public PackageAssignmentEntity? GetBagByBarcode(string barcode)
    {
        return _context.PackageAssignments.Single(x => x.Barcode == barcode);
    }

    public bool PackageInBag(string barcode)
    {
        return _context.PackageAssignments.Any(x => x.Barcode == barcode);
    }

    public IEnumerable<PackageAssignmentEntity> GetPackagesInsideBag(string barcode)
    {
        return _context.PackageAssignments.Where(x => x.BagBarcode == barcode);
    }
}