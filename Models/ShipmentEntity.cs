namespace FleetManagementApi.Models;

public class ShipmentEntity
{
    public Guid Id { get; set; }
    public string? Barcode { get; set; }
    public string? BagBarcode { get; set; }
}
