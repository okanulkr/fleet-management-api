using FleetManagementApi.Entities.PackageAssignment;
using FleetManagementApi.Entities.DeliveryPoint;
using FleetManagementApi.Entities.Package;
using FleetManagementApi.Entities.Vehicle;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Configuration;
using Serilog.Sinks.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.InMemory());

builder.Services.AddControllers();

builder.Services.AddDbContext<VehicleContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddDbContext<DeliveryPointContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddDbContext<PackageContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddDbContext<PackageAssignmentContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));

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
