using ParkingSystem.InfrastructureLayer.DbContextEF;

namespace ParkingSystem.ApplicationLayer.Interfaces
{
    public interface ISeedParkingDataService
	{
		void Initialize(ReservationDbContext dbContext);
	}
}
