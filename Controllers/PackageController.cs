using FleetManagementApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    private readonly ILogger<PackageController> _logger;
    PackageContext _dbContext;

    public PackageController(ILogger<PackageController> logger, PackageContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public IEnumerable<PackageEntity> GetAll()
    {
        return _dbContext.Packages;
    }

    [HttpGet("GetById")]
    public PackageEntity GetById(Guid id)
    {
        return _dbContext.Packages.Single(x => x.Id == id);
    }

    [HttpPost("CreatePackage")]
    public IActionResult CreatePackage(PackageCreateRequest request)
    {
        PackageEntity entity = new PackageEntity();
        entity.PackageType = PackageType.Package;
        entity.State = State.Created;
        entity.Barcode = request.Barcode;
        entity.DeliveryPoint = request.DeliveryPoint;
        entity.Weight = request.Weight;
        _dbContext.Add<PackageEntity>(entity);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id });
    }

    [HttpPost("CreateBag")]
    public IActionResult CreateBag(BagCreateRequest request)
    {
        PackageEntity entity = new PackageEntity();
        entity.PackageType = PackageType.Bag;
        entity.State = State.Created;
        entity.Barcode = request.Barcode;
        entity.DeliveryPoint = request.DeliveryPoint;
        _dbContext.Add<PackageEntity>(entity);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id });
    }
}
