namespace FleetManagementApi.Entities;

public class BagEntity
{
    public Guid Id { get; set; }
    public string? Barcode { get; set; }
    public int DeliveryPoint { get; set; }
    public State State { get; set; }
}
