using Microsoft.EntityFrameworkCore;
using ParkingSystem.ApplicationLayer.Interfaces;
using ParkingSystem.ApplicationLayer.Services;
using ParkingSystem.InfrastructureLayer.DbContextEF;
using ParkingSystem.InfrastructureLayer.Repositories;
using ParkingSystemAPI.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISeedParkingDataService, SeedParkingDataService>();
builder.Services.AddScoped(typeof(IParkingService), typeof(ParkingService));
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();

builder.Services.AddDbContext<ReservationDbContext>(opt =>
	opt.UseInMemoryDatabase("ParkingSystemDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	app.SeedData();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
