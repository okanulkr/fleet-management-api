using FleetManagementApi.Dto;
using FleetManagementApi.Entities;
using FleetManagementApi.Handlers.Package.Commands;
using FleetManagementApi.Handlers.Package.Query;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PackageController : ControllerBase
{
    BagCreateCommandHandler _createBagCommandHandler;
    PackageGetByIdQueryHandler _getByIdQueryHandler;
    PackageCreateCommandHandler _createPackageCommandHandler;
    public PackageController(
        PackageGetByIdQueryHandler queryByIdQueryHandler,
        PackageCreateCommandHandler createPackageCommandHandler,
        BagCreateCommandHandler createBagCommandHandler)
    {
        _getByIdQueryHandler = queryByIdQueryHandler;
        _createBagCommandHandler = createBagCommandHandler;
        _createPackageCommandHandler = createPackageCommandHandler;
    }

    [HttpGet("GetById")]
    public IActionResult GetById(string id)
    {
        PackageItemDto? dto = _getByIdQueryHandler.Handle(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPost("CreatePackage")]
    public IActionResult CreatePackage(PackageCreateRequest request)
    {
        PackageCreateResponse? response = _createPackageCommandHandler.Handle(request);
        return response == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = response.Barcode });
    }

    [HttpPost("CreateBag")]
    public IActionResult CreateBag(BagCreateRequest request)
    {
        BagCreateResponse? response = _createBagCommandHandler.Handle(request);
        return response == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = response.Barcode });
    }
}
