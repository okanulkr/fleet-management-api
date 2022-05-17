using FleetManagementApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageAssignmentController : ControllerBase
{
    private readonly ILogger<PackageAssignmentController> _logger;
    PackageAssignmentContext _dbContext;

    public PackageAssignmentController(ILogger<PackageAssignmentController> logger, PackageAssignmentContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
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
        PackageAssignmentEntity entity = new PackageAssignmentEntity();
        entity.Barcode = request.Barcode;
        entity.BagBarcode = request.BagBarcode;
        _dbContext.Add<PackageAssignmentEntity>(entity);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id });
    }
}
