namespace FleetManagementApi.Dto
{
    public class ShipmentCreateResponse
    {
        public string? Plate { get; set; }
        public IEnumerable<ShipmentRouteResponse>? Route { get; set; }
    }
}
