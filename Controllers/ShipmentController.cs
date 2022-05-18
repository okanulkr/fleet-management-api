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
        _packageContext.Add(new PackageEntity() { Barcode = "C725799", DeliveryPoint = 2 });
        _packageContext.Add(new PackageEntity() { Barcode = "C725800", DeliveryPoint = 3 });

        _packageContext.Add(new PackageEntity() { Barcode = "P7988000121", DeliveryPoint = 1, Weight = 5, });
        _packageContext.Add(new PackageEntity() { Barcode = "P7988000122", DeliveryPoint = 1, Weight = 5, });
        _packageContext.Add(new PackageEntity() { Barcode = "P7988000123", DeliveryPoint = 1, Weight = 9, });
        _packageContext.Add(new PackageEntity() { Barcode = "P8988000120", DeliveryPoint = 2, Weight = 33, });
        _packageContext.Add(new PackageEntity() { Barcode = "P8988000121", DeliveryPoint = 2, Weight = 17, });
        _packageContext.Add(new PackageEntity() { Barcode = "P8988000122", DeliveryPoint = 2, Weight = 26, });
        _packageContext.Add(new PackageEntity() { Barcode = "P8988000123", DeliveryPoint = 2, Weight = 35, });
        _packageContext.Add(new PackageEntity() { Barcode = "P8988000124", DeliveryPoint = 2, Weight = 1, });
        _packageContext.Add(new PackageEntity() { Barcode = "P8988000125", DeliveryPoint = 2, Weight = 200, });
        _packageContext.Add(new PackageEntity() { Barcode = "P8988000126", DeliveryPoint = 2, Weight = 50, });
        _packageContext.Add(new PackageEntity() { Barcode = "P9988000126", DeliveryPoint = 3, Weight = 15, });
        _packageContext.Add(new PackageEntity() { Barcode = "P9988000127", DeliveryPoint = 3, Weight = 16, });
        _packageContext.Add(new PackageEntity() { Barcode = "P9988000128", DeliveryPoint = 3, Weight = 55, });
        _packageContext.Add(new PackageEntity() { Barcode = "P9988000129", DeliveryPoint = 3, Weight = 28, });
        _packageContext.Add(new PackageEntity() { Barcode = "P9988000130", DeliveryPoint = 3, Weight = 17, });

        _packageAssignmentContext.Add(new PackageAssignmentEntity() { Barcode = "P8988000122", BagBarcode = "C725799", });
        _packageAssignmentContext.Add(new PackageAssignmentEntity() { Barcode = "P8988000126", BagBarcode = "C725799", });
        _packageAssignmentContext.Add(new PackageAssignmentEntity() { Barcode = "P9988000128", BagBarcode = "C725800", });
        _packageAssignmentContext.Add(new PackageAssignmentEntity() { Barcode = "P9988000129", BagBarcode = "C725800", });

        _packageContext.SaveChanges();
        _packageAssignmentContext.SaveChanges();

        // TODO
        // update all packages as 'loaded'
        // update related bags as 'loaded'

        ShipmentCreateResponse response = new ShipmentCreateResponse();
        response.Plate = request.Plate;
        response.Route = new List<ShipmentRouteResponse>();

        foreach (ShipmentRouteRequest point in request.Route!)
        {
            ShipmentRouteResponse updatedPoint = new ShipmentRouteResponse()
            {
                DeliveryPoint = point.DeliveryPoint,
                Deliveries = new List<DeliveryItemResponse>()
            };
            foreach (DeliveryItemRequest delivery in point.Deliveries!)
            {
                PackageEntity package = _packageContext.Packages.Single(x => x.Barcode == delivery.Barcode);
                package.State = State.Loaded;

                bool isBag = package.PackageType == PackageType.Bag;

                if (!isBag) // package type
                {

                    bool inBag = _packageAssignmentContext.PackageAssignments.Any(x => x.Barcode == package.Barcode);

                    // not in bag
                    if (!inBag && point.DeliveryPoint != 3 && point.DeliveryPoint == package.DeliveryPoint)
                    {
                        package.State = State.Unloaded;
                    }

                    // in bag
                    if (inBag)
                    {
                        PackageAssignmentEntity packageAssignments = _packageAssignmentContext.PackageAssignments
                                .Single(x => x.Barcode == package.Barcode);

                        PackageEntity bag = _packageContext.Packages.Single(x => x.Barcode == packageAssignments.BagBarcode);

                        if (bag.DeliveryPoint == point.DeliveryPoint && bag.DeliveryPoint == package.DeliveryPoint)
                        {
                            bag.State = State.Unloaded;
                            package.State = State.Unloaded;
                        }
                    }
                }
                else // bag type
                {
                    List<PackageAssignmentEntity> packageAssignments = _packageAssignmentContext.PackageAssignments
                            .Where(x => x.BagBarcode == package.Barcode)
                            .ToList();

                    if (point.DeliveryPoint != 1 && point.DeliveryPoint == package.DeliveryPoint)
                    {
                        package.State = State.Unloaded;
                        foreach (PackageAssignmentEntity packageAssignment in packageAssignments)
                        {
                            PackageEntity packageOfBag = _packageContext.Packages.Single(x => x.Barcode == packageAssignment.Barcode);
                            if (packageOfBag.DeliveryPoint == package.DeliveryPoint && packageOfBag.DeliveryPoint == point.DeliveryPoint)
                            {
                                packageOfBag.State = State.Unloaded;
                            }
                        }
                    }
                }

                if (package.State == State.Loaded)
                {
                    _logger.LogInformation($"{package.Barcode} not delivered due to wrong delivery point");
                }

                var deliveryItemResponse = new DeliveryItemResponse() { Barcode = package.Barcode, State = package.State };
                updatedPoint.Deliveries = updatedPoint.Deliveries.Append(deliveryItemResponse);
                _packageContext.SaveChanges();
            }
            response.Route = response.Route.Append(updatedPoint);
        }

        return Ok(response);
    }
}
