namespace FleetManagementApi.Dto
{
    public class ShipmentRouteRequest
    {
        public int DeliveryPoint { get; set; }
        public IEnumerable<DeliveryItemRequest>? Deliveries { get; set; }
    }
}