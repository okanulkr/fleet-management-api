namespace FleetManagementApi.Dto
{
    public class ShipmentRouteResponse
    {
        public int DeliveryPoint { get; set; }
        public IEnumerable<DeliveryItemResponse>? Deliveries { get; set; }
    }
}