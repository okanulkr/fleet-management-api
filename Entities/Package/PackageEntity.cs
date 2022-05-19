namespace FleetManagementApi.Entities.Package;

public class PackageEntity
{
    public Guid Id { get; set; }
    public string? Barcode { get; set; }
    public int DeliveryPoint { get; set; }
    public int Weight { get; set; }
    public State State { get; set; }
    public PackageType PackageType { get; set; }

    public void Load()
    {
        State = State.Loaded;
    }

    public void Unload()
    {
        State = State.Unloaded;
    }

    public bool UnloadableAt(params int[] deliveryPoints)
    {
        bool result = false;

        foreach (int deliveryPoint in deliveryPoints)
        {
            bool hasSameDestination = deliveryPoint == DeliveryPoint;
            bool suitableForPackageType = PackageType == PackageType.Bag ? deliveryPoint != 1 : deliveryPoint != 3;
            result |= hasSameDestination && suitableForPackageType;
        }

        return result;
    }
}
