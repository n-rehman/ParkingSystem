using Microsoft.EntityFrameworkCore;
using Moq;
using ParkingSystem.ApplicationLayer.Interfaces;
using ParkingSystem.ApplicationLayer.Services;
using ParkingSystem.Common.Requests;
using ParkingSystem.InfrastructureLayer.DbContextEF;
using ParkingSystem.InfrastructureLayer.Model;
using ParkingSystem.InfrastructureLayer.Repositories;
using System.Reflection.Metadata;

namespace ParkingSystem.ApplicationLayer.UnitTests
{
	[TestClass]
	public class ReservationRepoUnitTests1
	{

		private readonly Mock<ReservationDbContext> _mockDBContext;
		
		public ReservationRepoUnitTests1()
		{
			_mockDBContext = new Mock<ReservationDbContext>();

			mockDBContextStaticData();
		}

		private void mockDBContextStaticData()
		{

			var reservationData = new List<Reservation>
			{
				new Reservation { Id = 1, VehicleReg = "MV65GTR", DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(7), SlotId = 5, BookingPrice = 450, ParkingSlots= new ParkingSlot{  SlotId=1} },
			//	new Reservation { Id = 2, VehicleReg = "BT22YTR", DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(3), SlotId = 1, BookingPrice = 150 }
			}.AsQueryable();

			var mockSetReservation = new Mock<DbSet<Reservation>>();
			mockSetReservation.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationData.Provider);
			mockSetReservation.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationData.Expression);
			mockSetReservation.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationData.ElementType);
			mockSetReservation.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(() => reservationData.GetEnumerator());

			_mockDBContext.Setup(c => c.Reservations).Returns(mockSetReservation.Object);

			var priceTypesData = new List<PriceType>
			{
				new PriceType { Id = 1, Description = "Summer" },
				new PriceType { Id = 2, Description = "Winter" }
			}.AsQueryable();

			var mockSetPriceTypes = new Mock<DbSet<PriceType>>();
			mockSetPriceTypes.As<IQueryable<PriceType>>().Setup(m => m.Provider).Returns(priceTypesData.Provider);
			mockSetPriceTypes.As<IQueryable<PriceType>>().Setup(m => m.Expression).Returns(priceTypesData.Expression);
			mockSetPriceTypes.As<IQueryable<PriceType>>().Setup(m => m.ElementType).Returns(priceTypesData.ElementType);
			mockSetPriceTypes.As<IQueryable<PriceType>>().Setup(m => m.GetEnumerator()).Returns(() => priceTypesData.GetEnumerator());

			_mockDBContext.Setup(c => c.PriceType).Returns(mockSetPriceTypes.Object);


			//prices data
			var pricesData = new List<Price>
			{
				new Price { Id = 1, PriceTypeId = 1, DailyCharge = 20 },
				new Price { Id = 2, PriceTypeId = 2, DailyCharge = 30 }
			}.AsQueryable();

			var mockSetPrices = new Mock<DbSet<Price>>();
			mockSetPrices.As<IQueryable<Price>>().Setup(m => m.Provider).Returns(pricesData.Provider);
			mockSetPrices.As<IQueryable<Price>>().Setup(m => m.Expression).Returns(pricesData.Expression);
			mockSetPrices.As<IQueryable<Price>>().Setup(m => m.ElementType).Returns(pricesData.ElementType);
			mockSetPrices.As<IQueryable<Price>>().Setup(m => m.GetEnumerator()).Returns(() => pricesData.GetEnumerator());

			_mockDBContext.Setup(c => c.Prices).Returns(mockSetPrices.Object);


			//parking slots data
			var parkingSlotsData = new List<ParkingSlot>
			{
				new ParkingSlot { SlotId = 1, SlotName = "Slot1" },
				new ParkingSlot { SlotId = 2, SlotName = "Slot2" },
				new ParkingSlot { SlotId = 3, SlotName = "Slot3" },
				new ParkingSlot { SlotId = 4, SlotName = "Slot4" },
				new ParkingSlot { SlotId = 5, SlotName = "Slot5" },
				new ParkingSlot { SlotId = 6, SlotName = "Slot6" },
				new ParkingSlot { SlotId = 7, SlotName = "Slot7" },
				new ParkingSlot { SlotId = 8, SlotName = "Slot8" },
				new ParkingSlot { SlotId = 9, SlotName = "Slot9" },
				new ParkingSlot { SlotId = 10, SlotName = "Slot10" }
			}.AsQueryable();

			var mockSetParkingSlots = new Mock<DbSet<ParkingSlot>>();
			mockSetParkingSlots.As<IQueryable<ParkingSlot>>().Setup(m => m.Provider).Returns(parkingSlotsData.Provider);
			mockSetParkingSlots.As<IQueryable<ParkingSlot>>().Setup(m => m.Expression).Returns(parkingSlotsData.Expression);
			mockSetParkingSlots.As<IQueryable<ParkingSlot>>().Setup(m => m.ElementType).Returns(parkingSlotsData.ElementType);
			mockSetParkingSlots.As<IQueryable<ParkingSlot>>().Setup(m => m.GetEnumerator()).Returns(() => parkingSlotsData.GetEnumerator());

			_mockDBContext.Setup(c => c.ParkingSlots).Returns(mockSetParkingSlots.Object);

			//save static data
			_mockDBContext.Setup(c => c.SaveChanges());



		}
		[TestMethod]
		public void Create_Reservation_Save_ViaContext_Test_ViaRepo()
		{

			var mockSetReservation = new Mock<DbSet<Reservation>>();

			_mockDBContext.Setup(m => m.Reservations).Returns(mockSetReservation.Object);

			ReservationRepository _resRepo = new ReservationRepository(_mockDBContext.Object);
			var reservation =
				new Reservation { Id = 2, VehicleReg = "MV65GTR", DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(7), SlotId = 5, BookingPrice = 450, ParkingSlots = new ParkingSlot { SlotId = 1 } };


			_resRepo.BookReservation(reservation);

			mockSetReservation.Verify(m => m.Add(It.IsAny<Reservation>()), Times.Once());
			_mockDBContext.Verify(m => m.SaveChanges(), Times.Once());

			Assert.IsNotNull(_resRepo.GetReservationById(reservation.Id));
		}
	}
}