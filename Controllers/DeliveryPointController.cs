using FleetManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DeliveryPointController : ControllerBase
{
    private readonly ILogger<DeliveryPointController> _logger;
    DeliveryPointContext _dbContext;

    public DeliveryPointController(ILogger<DeliveryPointController> logger, DeliveryPointContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public IEnumerable<DeliveryPointEntity> GetAll()
    {
        return _dbContext.DeliveryPoints;
    }

    [HttpGet("GetById")]
    public DeliveryPointEntity GetById(Guid id)
    {
        return _dbContext.DeliveryPoints.Single(x => x.Id == id);
    }

    [HttpPost("Create")]
    public IActionResult Create(DeliveryPointCreateRequest request)
    {
        DeliveryPointEntity entity = new DeliveryPointEntity();
        entity.Name = request.Name;
        entity.Value = request.Value;
        _dbContext.Add<DeliveryPointEntity>(entity);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id });
    }
}
