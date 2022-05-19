using FleetManagementApi.Handlers.DeliveryPoint.Commands;
using FleetManagementApi.Handlers.DeliveryPoint.Query;
using FleetManagementApi.Dto.DeliveryPoint;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DeliveryPointController : ControllerBase
{
    DeliveryPointCreateCommandHandler _createCommandHandler;
    DeliveryPointGetByIdQueryHandler _getByIdQueryHandler;

    public DeliveryPointController(DeliveryPointCreateCommandHandler createCommandHandler, DeliveryPointGetByIdQueryHandler getByIdQueryHandler)
    {
        _createCommandHandler = createCommandHandler;
        _getByIdQueryHandler = getByIdQueryHandler;
    }

    [HttpGet("GetById")]
    public IActionResult GetById(int value)
    {
        DeliveryPointItemDto? dto = _getByIdQueryHandler.Handle(value);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPost("Create")]
    public IActionResult Create(DeliveryPointCreateRequest request)
    {
        DeliveryPointCreateResponse? response = _createCommandHandler.Handle(request);
        return response == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = response.Value });
    }
}
