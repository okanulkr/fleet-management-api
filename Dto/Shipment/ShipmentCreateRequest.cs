namespace FleetManagementApi.Dto
{
    public class ShipmentCreateRequest
    {
        public string? Plate { get; set; }
        public IEnumerable<ShipmentRouteRequest>? Route { get; set; }
    }
}
