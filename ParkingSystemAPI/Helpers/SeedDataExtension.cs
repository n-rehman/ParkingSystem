using ParkingSystem.ApplicationLayer.Interfaces;
using ParkingSystem.InfrastructureLayer.DbContextEF;

namespace ParkingSystemAPI.Helpers
{
    public static class SeedDataExtension
	{
		public static void SeedData(this WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<ReservationDbContext>();
				var seedDataService = scope.ServiceProvider.GetRequiredService<ISeedParkingDataService>();

				seedDataService.Initialize(dbContext);
			}
		}
	}
}
