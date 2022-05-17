using FleetManagementApi.Entities;
using FleetManagementApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ShipmentController : ControllerBase
{
    private readonly ILogger<ShipmentController> _logger;
    PackageContext _packageContext;
    PackageAssignmentContext _packageAssignmentContext;

    public ShipmentController(
        ILogger<ShipmentController> logger,
        PackageContext packageContext,
        PackageAssignmentContext packageAssignmentContext)
    {
        _logger = logger;
        _packageContext = packageContext;
        _packageAssignmentContext = packageAssignmentContext;
    }

    [HttpPost("Ship")]
    public IActionResult Ship(ShipmentCreateRequest request)
    {
        // TODO
        // update all packages as 'loaded'
        // update related bags as 'loaded'

        foreach (ShipmentRouteRequest point in request.Route!)
        {
            foreach (DeliveryItemRequest delivery in point.Deliveries!)
            {
                PackageEntity package = _packageContext.Packages.Single(x => x.Barcode == delivery.Barcode);
                package.State = State.Loaded;

                // package type
                if (point.DeliveryPoint != 3)
                {
                    package.State = State.Unloaded;
                }

                // bag type
                if (point.DeliveryPoint != 1)
                {
                    List<PackageAssignmentEntity> packageAssignments = _packageAssignmentContext.PackageAssignments
                        .Where(x => x.BagBarcode == package.Barcode)
                        .ToList();

                    package.State = State.Unloaded;
                    foreach (PackageAssignmentEntity packageAssignment in packageAssignments)
                    {
                        PackageEntity packageOfBag = _packageContext.Packages.Single(x => x.Barcode == packageAssignment.Barcode);
                        packageOfBag.State = State.Unloaded;
                    }
                }
            }
        }

        return Ok();
    }
}
