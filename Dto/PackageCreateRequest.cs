namespace FleetManagementApi.Models
{
    public class PackageCreateRequest
    {
        public string? Barcode { get; set; }
        public int DeliveryPoint { get; set; }
        public int Weight { get; set; }
    }
}
