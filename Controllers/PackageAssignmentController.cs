using FleetManagementApi.Handlers.PackageAssignment.Query;
using Microsoft.AspNetCore.Mvc;
using FleetManagementApi.Handlers.PackageAssignment.Command;
using FleetManagementApi.Dto.PackageAssignment;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageAssignmentController : ControllerBase
{
    PackageAssignmentGetByIdQueryHandler _getByIdQueryHandler;
    PackageAssignmentCreateCommandHandler _createCommandHandler;

    public PackageAssignmentController(PackageAssignmentGetByIdQueryHandler getByIdQueryHandler, PackageAssignmentCreateCommandHandler createCommandHandler)
    {
        _getByIdQueryHandler = getByIdQueryHandler;
        _createCommandHandler = createCommandHandler;
    }

    [HttpGet("GetById")]
    public IActionResult GetById(string id)
    {
        PackageAssignmentItemDto? dto = _getByIdQueryHandler.Handle(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPost("Create")]
    public IActionResult Create(PackageAssignmentCreateRequest request)
    {
        PackageAssignmentCreateResponse? response = _createCommandHandler.Handle(request);
        return response == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = response.CompositeId });
    }
}
