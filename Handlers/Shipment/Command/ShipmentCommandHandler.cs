using FleetManagementApi.Entities.Package;
using FleetManagementApi.Entities.PackageAssignment;
using FleetManagementApi.Repositories.PackageAssignment;
using FleetManagementApi.Repositories.Package;
using FleetManagementApi.Dto.Shipment.Response;
using Response = FleetManagementApi.Dto.Shipment.Response;
using Request = FleetManagementApi.Dto.Shipment.Request;

namespace FleetManagementApi.Handlers.Shipment.Command
{
    public class ShipmentCommandHandler
    {
        private readonly ILogger<ShipmentCommandHandler> _logger;
        private readonly IPackageRepository _packageRepository;
        private readonly IPackageAssignmentRepository _packageAssignmentRepository;

        public ShipmentCommandHandler(ILogger<ShipmentCommandHandler> logger, IPackageRepository packageRepository, IPackageAssignmentRepository packageAssignmentRepository)
        {
            _logger = logger;
            _packageRepository = packageRepository;
            _packageAssignmentRepository = packageAssignmentRepository;
        }

        public Response.ShipmentResponse? Handle(Request.ShipmentRequest shipment)
        {
            // TODO: add existince checks, return null if any resource not found
            _packageRepository.Add(new PackageEntity() { Barcode = "C725799", DeliveryPoint = 2, PackageType = PackageType.Bag });
            _packageRepository.Add(new PackageEntity() { Barcode = "C725800", DeliveryPoint = 3, PackageType = PackageType.Bag });

            _packageRepository.Add(new PackageEntity() { Barcode = "P7988000121", PackageType = PackageType.Package, DeliveryPoint = 1, Weight = 5, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P7988000122", PackageType = PackageType.Package, DeliveryPoint = 1, Weight = 5, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P7988000123", PackageType = PackageType.Package, DeliveryPoint = 1, Weight = 9, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000120", PackageType = PackageType.Package, DeliveryPoint = 2, Weight = 33, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000121", PackageType = PackageType.Package, DeliveryPoint = 2, Weight = 17, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000122", PackageType = PackageType.Package, DeliveryPoint = 2, Weight = 26, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000123", PackageType = PackageType.Package, DeliveryPoint = 2, Weight = 35, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000124", PackageType = PackageType.Package, DeliveryPoint = 2, Weight = 1, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000125", PackageType = PackageType.Package, DeliveryPoint = 2, Weight = 200, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P8988000126", PackageType = PackageType.Package, DeliveryPoint = 2, Weight = 50, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000126", PackageType = PackageType.Package, DeliveryPoint = 3, Weight = 15, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000127", PackageType = PackageType.Package, DeliveryPoint = 3, Weight = 16, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000128", PackageType = PackageType.Package, DeliveryPoint = 3, Weight = 55, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000129", PackageType = PackageType.Package, DeliveryPoint = 3, Weight = 28, });
            _packageRepository.Add(new PackageEntity() { Barcode = "P9988000130", PackageType = PackageType.Package, DeliveryPoint = 3, Weight = 17, });

            _packageAssignmentRepository.Add(new PackageAssignmentEntity() { Barcode = "P8988000122", BagBarcode = "C725799", });
            _packageAssignmentRepository.Add(new PackageAssignmentEntity() { Barcode = "P8988000126", BagBarcode = "C725799", });
            _packageAssignmentRepository.Add(new PackageAssignmentEntity() { Barcode = "P9988000128", BagBarcode = "C725800", });
            _packageAssignmentRepository.Add(new PackageAssignmentEntity() { Barcode = "P9988000129", BagBarcode = "C725800", });

            var response = new Response.ShipmentResponse()
            {
                Plate = shipment.Plate,
                Route = new List<Response.ShipmentRoute>()
            };

            foreach (var point in shipment.Route!)
            {
                TravelRoute(response, point);
            }

            return response;
        }

        private void TravelRoute(ShipmentResponse response, Request.ShipmentRoute point)
        {
            var updatedPoint = new Response.ShipmentRoute()
            {
                DeliveryPoint = point.DeliveryPoint,
                Deliveries = new List<Response.Package>()
            };
            foreach (var delivery in point.Deliveries!)
            {
                VisitDeliveryPoint(point, updatedPoint, delivery);
            }
            response.Route = response.Route!.Append(updatedPoint);
        }

        private void VisitDeliveryPoint(Request.ShipmentRoute point, ShipmentRoute updatedPoint, Request.Package delivery)
        {
            PackageEntity package = _packageRepository.GetByBarcode(delivery.Barcode!)!;
            package.Load();

            bool isBag = package.PackageType == PackageType.Bag;
            if (isBag)
            {
                TryUnloadBag(point, package);
            }
            else
            {
                TryUnloadPackage(point, package);
            }

            bool unloaded = package.State != State.Loaded;
            if (!unloaded)
            {
                _logger.LogInformation($"{package.Barcode} not delivered due to wrong delivery point");
            }

            var deliveryItemResponse = new Response.Package() { Barcode = package.Barcode, State = package.State };
            updatedPoint.Deliveries = updatedPoint.Deliveries!.Append(deliveryItemResponse);
            _packageRepository.SaveChanges();
        }

        private void TryUnloadBag(Request.ShipmentRoute point, PackageEntity bag)
        {
            if (bag.UnloadableAt(point.DeliveryPoint))
            {
                bag.Unload();
                TryUnloadBagContent(point, bag);
            }
        }

        private void TryUnloadBagContent(Request.ShipmentRoute point, PackageEntity bag)
        {
            List<PackageAssignmentEntity> packageAssignments = _packageAssignmentRepository.GetPackagesInsideBag(bag.Barcode!).ToList();
            foreach (PackageAssignmentEntity packageAssignment in packageAssignments)
            {
                PackageEntity packageInsideOfBag = _packageRepository.GetByBarcode(packageAssignment.Barcode!)!;
                if (packageInsideOfBag.UnloadableAt(point.DeliveryPoint, bag.DeliveryPoint))
                {
                    packageInsideOfBag.Unload();
                }
            }
        }

        private void TryUnloadPackage(Request.ShipmentRoute point, PackageEntity package)
        {
            bool packageInBag = _packageAssignmentRepository.PackageInBag(package.Barcode!);
            if (packageInBag)
            {
                TryUnloadWithBag(point, package);
            }
            else if (package.UnloadableAt(point.DeliveryPoint))
            {
                package.Unload();
            }
        }

        private void TryUnloadWithBag(Request.ShipmentRoute point, PackageEntity package)
        {
            PackageAssignmentEntity packageAssignments = _packageAssignmentRepository.GetBagByBarcode(package.Barcode!)!;
            PackageEntity bag = _packageRepository.GetByBarcode(packageAssignments.BagBarcode!)!;
            if (bag.UnloadableAt(point.DeliveryPoint, package.DeliveryPoint))
            {
                bag.Unload();
                package.Unload();
            }
        }
    }
}