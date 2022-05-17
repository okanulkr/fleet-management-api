using FleetManagementApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageAssignmentController : ControllerBase
{
    private readonly ILogger<PackageAssignmentController> _logger;
    PackageAssignmentContext _dbContext;
    PackageContext _packageDbContext;

    public PackageAssignmentController(ILogger<PackageAssignmentController> logger, PackageAssignmentContext dbContext, PackageContext packageDbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        _packageDbContext = packageDbContext;
    }

    [HttpGet("GetAll")]
    public IEnumerable<PackageAssignmentEntity> GetAll()
    {
        return _dbContext.PackageAssignments;
    }

    [HttpGet("GetById")]
    public PackageAssignmentEntity GetById(Guid id)
    {
        return _dbContext.PackageAssignments.Single(x => x.Id == id);
    }

    [HttpPost("Create")]
    public IActionResult Create(PackageAssignmentCreateRequest request)
    {
        PackageEntity package = _packageDbContext.Packages.Single(x => x.Barcode == request.Barcode);
        package.State = State.LoadedIntoBag;
        _packageDbContext.SaveChanges();

        PackageAssignmentEntity entity = new PackageAssignmentEntity();
        entity.Barcode = request.Barcode;
        entity.BagBarcode = request.BagBarcode;

        _dbContext.Add<PackageAssignmentEntity>(entity);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id });
    }
}
