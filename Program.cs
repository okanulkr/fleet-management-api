using FleetManagementApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<VehicleContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddDbContext<DeliveryPointContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddDbContext<BagContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));
builder.Services.AddDbContext<PackageContext>(opt => opt.UseInMemoryDatabase("FleetManagementDb"));

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
