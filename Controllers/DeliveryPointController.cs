using FleetManagementApi.Dto;
using FleetManagementApi.Entities;
using FleetManagementApi.Handlers.DeliveryPoint.Commands;
using FleetManagementApi.Handlers.DeliveryPoint.Query;
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
        DeliveryPointCreateResponse response = _createCommandHandler.Handle(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Value });
    }
}
