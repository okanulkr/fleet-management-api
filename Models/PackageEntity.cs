namespace FleetManagementApi.Models;

public class PackageEntity
{
    public Guid Id { get; set; }
    public string? Barcode { get; set; }
    public int DeliveryPoint { get; set; }
    public int Weight { get; set; }
}
