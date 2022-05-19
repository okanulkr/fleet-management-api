using FleetManagementApi.Entities.DeliveryPoint;

namespace FleetManagementApi.Dto.DeliveryPoint;

public class DeliveryPointItemDto
{
    public string? Name { get; set; }
    public int Value { get; set; }

    internal static DeliveryPointItemDto? MapFrom(DeliveryPointEntity? entity)
    {
        DeliveryPointItemDto? dto = null;

        if (entity != null)
        {
            dto = new DeliveryPointItemDto()
            {
                Name = entity.Name,
                Value = entity.Value
            };
        }

        return dto;
    }
}
