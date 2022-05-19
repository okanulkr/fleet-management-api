
using Microsoft.AspNetCore.Mvc;
using FleetManagementApi.Handlers.Vehicle.Command;
using FleetManagementApi.Handlers.Vehicle.Query;
using FleetManagementApi.Dto.Vehicle;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    VehicleCreateCommandHandler _createCommandHandler;
    VehicleGetByIdQueryHandler _getByIdQueryHandler;

    public VehicleController(VehicleCreateCommandHandler createCommandHandler, VehicleGetByIdQueryHandler getByIdQueryHandler)
    {
        _createCommandHandler = createCommandHandler;
        _getByIdQueryHandler = getByIdQueryHandler;
    }

    [HttpGet("GetById")]
    public IActionResult GetById(string licensePlate)
    {
        VehicleItemDto? dto = _getByIdQueryHandler.Handle(licensePlate);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPost("Create")]
    public IActionResult Create(VehicleCreateRequest request)
    {
        VehicleCreateResponse? response = _createCommandHandler.Handle(request);
        return response == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = response.LicensePlate });
    }
}
