using ParkingSystem.ApplicationLayer.Interfaces;
using ParkingSystem.InfrastructureLayer.DbContextEF;
using ParkingSystem.InfrastructureLayer.Model;

namespace ParkingSystem.ApplicationLayer.Services
{
	public class SeedParkingDataService : ISeedParkingDataService
	{
		public void Initialize(ReservationDbContext context)
		{

			//Initial Data Mockup

			//reservations

			context.Reservations.Add(new Reservation { Id = 1, VehicleReg = "MV65GTR", DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(7), SlotId = 5, BookingPrice = 450 }); ;
			context.Reservations.Add(new Reservation { Id = 2, VehicleReg = "BT22YTR", DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(3), SlotId = 1, BookingPrice = 150 });


			//prices by types
			context.Prices.Add(new Price { Id = 1, PriceType = "Summer", DailyCharge = 20, DateFrom = DateTime.Parse("2023-04-01"), DateTo=DateTime.Parse("2023-09-30") });
			context.Prices.Add(new Price { Id = 2, PriceType = "Winter", DailyCharge = 30, DateFrom = DateTime.Parse("2022-10-01"), DateTo = DateTime.Parse("2023-03-31") });

			//parking slots
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 1, SlotName = "Slot1" });
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 2, SlotName = "Slot2" });
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 3, SlotName = "Slot3" });
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 4, SlotName = "Slot4" });
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 5, SlotName = "Slot5" });
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 6, SlotName = "Slot6" });
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 7, SlotName = "Slot7" });
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 8, SlotName = "Slot8" });
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 9, SlotName = "Slot9" });
			context.ParkingSlots.Add(new ParkingSlot { SlotId = 10, SlotName = "Slot10" });

			context.SaveChanges();
		}
	}
}
