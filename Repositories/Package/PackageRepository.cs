using FleetManagementApi.Entities.Package;

namespace FleetManagementApi.Repositories.Package;

public class PackageRepository : IPackageRepository
{
    private readonly PackageContext _context;

    public PackageRepository(PackageContext context) { _context = context; }

    public string? Add(PackageEntity package)
    {
        _context.Add(package);
        SaveChanges();
        return package.Barcode;
    }

    public PackageEntity? GetByBarcode(string barcode)
    {
        return _context.Packages.SingleOrDefault(x => x.Barcode == barcode);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}