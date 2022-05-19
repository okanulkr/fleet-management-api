using FleetManagementApi.Entities.Package;
using FleetManagementApi.Entities.PackageAssignment;
using Response = FleetManagementApi.Dto.Shipment.Response;
using Request = FleetManagementApi.Dto.Shipment.Request;
using FleetManagementApi.Repositories.PackageAssignment;
using FleetManagementApi.Repositories.Package;

namespace FleetManagementApi.Handlers.Shipment.Command
{
    public class ShipmentCommandHandler
    {
        private readonly ILogger<ShipmentCommandHandler> _logger;
        IPackageRepository _packageRepository;
        IPackageAssignmentRepository _packageAssignmentRepository;

        public ShipmentCommandHandler(ILogger<ShipmentCommandHandler> logger, IPackageRepository packageRepository, IPackageAssignmentRepository packageAssignmentRepository)
        {
            _logger = logger;
            _packageRepository = packageRepository;
            _packageAssignmentRepository = packageAssignmentRepository;
        }

        public Response.ShipmentResponse? Handle(Request.ShipmentRequest shipment)
        {
            // TODO: add existince checks, return null if any resource not found
            _packageRepository.Add(new PackageEntity() { Barcode = "C725799", DeliveryPoint = 2 });
            _packageRepository.Add(new PackageEntity() { Barcode = "C725800", DeliveryPoint = 3 });

            _packageRepository.Add(new PackageEntity() { Barcode = "P7988000121", DeliveryPoint = 1, Weight = 5, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P7988000122", DeliveryPoint = 1, Weight = 5, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P7988000123", DeliveryPoint = 1, Weight = 9, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000120", DeliveryPoint = 2, Weight = 33, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000121", DeliveryPoint = 2, Weight = 17, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000122", DeliveryPoint = 2, Weight = 26, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000123", DeliveryPoint = 2, Weight = 35, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000124", DeliveryPoint = 2, Weight = 1, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000125", DeliveryPoint = 2, Weight = 200, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000126", DeliveryPoint = 2, Weight = 50, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000126", DeliveryPoint = 3, Weight = 15, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000127", DeliveryPoint = 3, Weight = 16, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000128", DeliveryPoint = 3, Weight = 55, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000129", DeliveryPoint = 3, Weight = 28, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000130", DeliveryPoint = 3, Weight = 17, });

            _packageAssignmentRepository.Add(new PackageAssignmentEntity() { Barcode = "P8988000122", BagBarcode = "C725799", });
            _packageAssignmentRepository.Add(new PackageAssignmentEntity() { Barcode = "P8988000126", BagBarcode = "C725799", });
            _packageAssignmentRepository.Add(new PackageAssignmentEntity() { Barcode = "P9988000128", BagBarcode = "C725800", });
            _packageAssignmentRepository.Add(new PackageAssignmentEntity() { Barcode = "P9988000129", BagBarcode = "C725800", });

            var response = new Response.ShipmentResponse();
            response.Plate = shipment.Plate;
            response.Route = new List<Response.ShipmentRoute>();

            foreach (var point in shipment.Route!)
            {
                var updatedPoint = new Response.ShipmentRoute()
                {
                    DeliveryPoint = point.DeliveryPoint,
                    Deliveries = new List<Response.Package>()
                };
                foreach (var delivery in point.Deliveries!)
                {
                    PackageEntity package = _packageRepository.GetByBarcode(delivery.Barcode!)!;
                    package.State = State.Loaded;

                    bool isBag = package.PackageType == PackageType.Bag;

                    if (!isBag) // package type
                    {

                        bool inBag = _packageAssignmentRepository.PackageInBag(package.Barcode!);

                        // not in bag
                        if (!inBag && point.DeliveryPoint != 3 && point.DeliveryPoint == package.DeliveryPoint)
                        {
                            package.State = State.Unloaded;
                        }

                        // in bag
                        if (inBag)
                        {
                            PackageAssignmentEntity packageAssignments = _packageAssignmentRepository.GetBagByBarcode(package.Barcode!)!;

                            PackageEntity bag = _packageRepository.GetByBarcode(packageAssignments.BagBarcode!)!;

                            if (bag.DeliveryPoint == point.DeliveryPoint && bag.DeliveryPoint == package.DeliveryPoint)
                            {
                                bag.State = State.Unloaded;
                                package.State = State.Unloaded;
                            }
                        }
                    }
                    else // bag type
                    {
                        List<PackageAssignmentEntity> packageAssignments = _packageAssignmentRepository.GetPackagesInsideBag(package.Barcode!).ToList();

                        if (point.DeliveryPoint != 1 && point.DeliveryPoint == package.DeliveryPoint)
                        {
                            package.State = State.Unloaded;
                            foreach (PackageAssignmentEntity packageAssignment in packageAssignments)
                            {
                                PackageEntity packageOfBag = _packageRepository.GetByBarcode(packageAssignment.Barcode!)!;
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

                    var deliveryItemResponse = new Response.Package() { Barcode = package.Barcode, State = package.State };
                    updatedPoint.Deliveries = updatedPoint.Deliveries.Append(deliveryItemResponse);
                    _packageRepository.SaveChanges();
                }
                response.Route = response.Route.Append(updatedPoint);
            }

            return response;
        }
    }
}