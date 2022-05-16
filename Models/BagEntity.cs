namespace FleetManagementApi.Models;

public class BagEntity
{
    public Guid Id { get; set; }
    public string? Barcode { get; set; }
    public int? DeliveryPoint { get; set; }
}
