using FleetManagementApi.Entities.Package;

namespace FleetManagementApi.Repositories.Package;

public interface IPackageRepository
{
    public string? Add(PackageEntity package);
    public PackageEntity? GetByBarcode(string barcode);
    public void SaveChanges();
}