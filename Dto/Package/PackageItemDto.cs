using FleetManagementApi.Entities;

namespace FleetManagementApi.Dto;

public class PackageItemDto
{
    public string? Barcode { get; set; }
    public int DeliveryPoint { get; set; }
    public int Weight { get; set; }
    public State State { get; set; }
    public PackageType PackageType { get; set; }

    internal static PackageItemDto? MapFrom(PackageEntity? entity)
    {
        PackageItemDto? dto = null;

        if (entity != null)
        {
            dto = new PackageItemDto()
            {
                Barcode = entity.Barcode,
                DeliveryPoint = entity.DeliveryPoint,
                PackageType = entity.PackageType,
                State = entity.State,
                Weight = entity.Weight
            };
        }

        return dto;
    }
}
