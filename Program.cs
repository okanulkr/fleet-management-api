using FleetManagementApi.Entities.PackageAssignment;
using FleetManagementApi.Entities.DeliveryPoint;
using FleetManagementApi.Entities.Package;
using FleetManagementApi.Entities.Vehicle;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.InMemory;
using FleetManagementApi.Handlers.DeliveryPoint.Commands;
using FleetManagementApi.Handlers.DeliveryPoint.Query;
using FleetManagementApi.Handlers.Package.Commands;
using FleetManagementApi.Handlers.Package.Query;
using FleetManagementApi.Handlers.PackageAssignment.Command;
using FleetManagementApi.Handlers.PackageAssignment.Query;
using FleetManagementApi.Handlers.Shipment.Command;
using FleetManagementApi.Handlers.Vehicle.Command;
using FleetManagementApi.Handlers.Vehicle.Query;
using FleetManagementApi.Repositories.DeliveryPoint;
using FleetManagementApi.Repositories.Vehicle;
using FleetManagementApi.Repositories.Package;
using FleetManagementApi.Repositories.PackageAssignment;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.InMemory());

builder.Services.AddControllers();

// IoC of Repositories and Command Query Handlers etc.

builder.Services.AddDbContext<DeliveryPointContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddSingleton<DeliveryPointCreateCommandHandler>();
builder.Services.AddSingleton<DeliveryPointGetByIdQueryHandler>();
builder.Services.AddSingleton<IDeliveryPointRepository, DeliveryPointRepository>();

builder.Services.AddDbContext<VehicleContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddSingleton<VehicleCreateCommandHandler>();
builder.Services.AddSingleton<VehicleGetByIdQueryHandler>();
builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();

builder.Services.AddDbContext<PackageAssignmentContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddSingleton<PackageAssignmentCreateCommandHandler>();
builder.Services.AddSingleton<PackageAssignmentGetByIdQueryHandler>();
builder.Services.AddSingleton<IPackageAssignmentRepository, PackageAssignmentRepository>();

builder.Services.AddDbContext<PackageContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddSingleton<PackageCreateCommandHandler>();
builder.Services.AddSingleton<BagCreateCommandHandler>();
builder.Services.AddSingleton<PackageGetByIdQueryHandler>();
builder.Services.AddSingleton<IPackageRepository, PackageRepository>();

builder.Services.AddSingleton<ShipmentCommandHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
