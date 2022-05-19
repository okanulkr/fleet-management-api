using FleetManagementApi.Entities.Vehicle;

namespace FleetManagementApi.Dto.Vehicle;

public class VehicleItemDto
{
    public string? LicensePlate { get; set; }

    internal static VehicleItemDto? MapFrom(VehicleEntity? entity)
    {
        VehicleItemDto? dto = null;

        if (entity != null)
        {
            dto = new VehicleItemDto()
            {
                LicensePlate = entity.LicensePlate
            };
        }

        return dto;
    }
}
