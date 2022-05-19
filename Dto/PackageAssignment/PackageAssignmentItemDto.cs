using FleetManagementApi.Entities.PackageAssignment;

namespace FleetManagementApi.Dto.PackageAssignment;

public class PackageAssignmentItemDto
{
    public string? Barcode { get; set; }
    public string? BagBarcode { get; set; }

    internal static PackageAssignmentItemDto? MapFrom(PackageAssignmentEntity? entity)
    {
        PackageAssignmentItemDto? dto = null;

        if (entity != null)
        {
            dto = new PackageAssignmentItemDto()
            {
                Barcode = entity.Barcode,
                BagBarcode = entity.BagBarcode
            };
        }

        return dto;
    }
}
