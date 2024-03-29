using Microsoft.AspNetCore.Mvc;
using FleetManagementApi.Handlers.Shipment.Command;
using FleetManagementApi.Dto.Shipment.Request;
using FleetManagementApi.Dto.Shipment.Response;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ShipmentController : ControllerBase
{
    private readonly ShipmentCommandHandler _commandHandler;

    public ShipmentController(ShipmentCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    [HttpPost("Ship")]
    public IActionResult Ship(ShipmentRequest request)
    {
        ShipmentResponse? response = _commandHandler.Handle(request);
        return response == null ? BadRequest() : Ok(response);
    }
}
