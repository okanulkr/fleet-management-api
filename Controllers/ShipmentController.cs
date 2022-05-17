using FleetManagementApi.Entities;
using FleetManagementApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ShipmentController : ControllerBase
{
    private readonly ILogger<ShipmentController> _logger;

    public ShipmentController(ILogger<ShipmentController> logger)
    {
        _logger = logger;
    }

    [HttpPost("Ship")]
    public IActionResult Ship(ShipmentCreateRequest request)
    {
        // TODO
        // update all packages as 'loaded'
        // update related bags as 'loaded'
        return Ok();
    }
}
