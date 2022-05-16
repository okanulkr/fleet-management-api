using FleetManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BagController : ControllerBase
{
    private readonly ILogger<BagController> _logger;
    BagContext _dbContext;

    public BagController(ILogger<BagController> logger, BagContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public IEnumerable<BagEntity> GetAll()
    {
        return _dbContext.Bags;
    }

    [HttpGet("GetById")]
    public BagEntity GetById(Guid id)
    {
        return _dbContext.Bags.Single(x => x.Id == id);
    }

    [HttpPost("Create")]
    public IActionResult Create(BagCreateRequest request)
    {
        BagEntity entity = new BagEntity();
        entity.Barcode = request.Barcode;
        entity.DeliveryPoint = request.DeliveryPoint;
        _dbContext.Add<BagEntity>(entity);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id });
    }
}
