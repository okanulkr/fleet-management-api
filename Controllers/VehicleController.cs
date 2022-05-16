using FleetManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly ILogger<VehicleController> _logger;
    VehicleContext _dbContext;

    public VehicleController(ILogger<VehicleController> logger, VehicleContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public IEnumerable<VehicleEntity> GetAll()
    {
        return _dbContext.Vehicles;
    }

    [HttpGet("GetById")]
    public VehicleEntity GetById(Guid id)
    {
        return _dbContext.Vehicles.Single(x => x.Id == id);
    }

    [HttpPost("Create")]
    public IActionResult Create(VehicleCreateRequest request)
    {
        VehicleEntity entity = new VehicleEntity();
        entity.LicensePlate = request.LicensePlate;
        _dbContext.Add<VehicleEntity>(entity);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id });
    }
}
