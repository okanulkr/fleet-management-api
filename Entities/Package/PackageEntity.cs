namespace FleetManagementApi.Entities;

public class PackageEntity
{
    public Guid Id { get; set; }
    public string? Barcode { get; set; }
    public int DeliveryPoint { get; set; }
    public int Weight { get; set; }
    public State State { get; set; }
    public PackageType PackageType { get; set; }
}
