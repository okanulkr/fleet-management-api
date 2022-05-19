using FleetManagementApi.Entities;

public interface IPackageRepository
{
    public string? Add(PackageEntity package);
    public PackageEntity? GetByBarcode(string barcode);
    public void SaveChanges();
}