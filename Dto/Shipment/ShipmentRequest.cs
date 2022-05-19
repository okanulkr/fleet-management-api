namespace FleetManagementApi.Dto.Shipment.Request
{
    public class ShipmentRequest
    {
        public string? Plate { get; set; }
        public IEnumerable<ShipmentRoute>? Route { get; set; }
    }

    public class ShipmentRoute
    {
        public int DeliveryPoint { get; set; }
        public IEnumerable<Package>? Deliveries { get; set; }
    }

    public class Package
    {
        public string? Barcode { get; set; }
    }
}
