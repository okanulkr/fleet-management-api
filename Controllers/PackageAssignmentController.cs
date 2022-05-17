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
        // Update package state as 'LoadedIntoBag'
        PackageEntity package = _packageDbContext.Packages.Single(x => x.Barcode == request.Barcode);
        package.State = State.LoadedIntoBag;

        // Increment weight of bag by package weight
        PackageEntity bag = _packageDbContext.Packages.Single(x => x.Barcode == request.BagBarcode);
        bag.Weight += package.Weight;

        _packageDbContext.SaveChanges();

        // Add package to bag assignment
        PackageAssignmentEntity entity = new PackageAssignmentEntity()
        {
            Barcode = request.Barcode,
            BagBarcode = request.BagBarcode
        };
        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id });
    }
}
