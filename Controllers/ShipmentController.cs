using FleetManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ShipmentController : ControllerBase
{
    private readonly ILogger<ShipmentController> _logger;
    ShipmentContext _dbContext;

    public ShipmentController(ILogger<ShipmentController> logger, ShipmentContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public IEnumerable<ShipmentEntity> GetAll()
    {
        return _dbContext.Shipments;
    }

    [HttpGet("GetById")]
    public ShipmentEntity GetById(Guid id)
    {
        return _dbContext.Shipments.Single(x => x.Id == id);
    }

    [HttpPost("Create")]
    public IActionResult Create(ShipmentCreateRequest request)
    {
        ShipmentEntity entity = new ShipmentEntity();
        entity.Barcode = request.Barcode;
        entity.BagBarcode = request.BagBarcode;
        _dbContext.Add<ShipmentEntity>(entity);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id });
    }
}
