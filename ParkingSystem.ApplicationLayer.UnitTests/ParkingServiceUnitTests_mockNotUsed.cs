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
	public class ParkingServiceUnitTests1
	{
		private  Mock<ReservationRepository> _mockReservationRepository;
		private readonly Mock<ISeedParkingDataService> _mockSeedData;
		private readonly Mock<ReservationDbContext> _mockDBContext;
		
		public ParkingServiceUnitTests1()
		{

			_mockReservationRepository= new Mock<ReservationRepository>();
			_mockDBContext = new Mock<ReservationDbContext>();

			//initialiseDBContextAndSeedData();
			mockDBContextStaticData();
		}

		private void mockDBContextStaticData()
		{
			//_mockDBContext = new Mock<ReservationDbContext>();



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
		private void initialiseDBContextAndSeedData()
		{



			var options = new DbContextOptionsBuilder<ReservationDbContext>()
			.UseInMemoryDatabase(databaseName: "ParkingSystemAPITestDB")
			.Options;

			var context = new ReservationDbContext(options);


			context.Reservations.Add(new Reservation() { Id = 1, VehicleReg = "MV65GTR", DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(7), SlotId = 5, BookingPrice = 450 }); ;
			context.Reservations.Add(new Reservation() { Id = 2, VehicleReg = "BT22YTR", DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(3), SlotId = 1, BookingPrice = 150 });

			//price types
			context.PriceType.Add(new PriceType() { Id = 1, Description = "Summer" });
			context.PriceType.Add(new PriceType() { Id = 2, Description = "Winter" });

			//prices by types
			context.Prices.Add(new Price() { Id = 1, PriceTypeId = 1, DailyCharge = 50 });
			context.Prices.Add(new Price() { Id = 2, PriceTypeId = 2, DailyCharge = 55 });

			//parking slots
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 1, SlotName = "Slot1" });
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 2, SlotName = "Slot2" });
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 3, SlotName = "Slot3" });
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 4, SlotName = "Slot4" });
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 5, SlotName = "Slot5" });
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 6, SlotName = "Slot6" });
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 7, SlotName = "Slot7" });
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 8, SlotName = "Slot8" });
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 9, SlotName = "Slot9" });
			context.ParkingSlots.Add(new ParkingSlot() { SlotId = 10, SlotName = "Slot10" });

			context.SaveChanges();

			//_parkingServiceMockDbContext = context;

		}
		[TestMethod]
		public void TestDBContextAndDataSeedInitialisation()
		{

			
			var repContext = new ReservationRepository(_mockDBContext.Object);
			var parkingService = new ParkingService(repContext);

			var availablity = parkingService.GetParkingSpaceAvailablity(DateTime.Now, DateTime.Now.AddDays(5));

			Assert.IsNotNull(availablity);

			var availableSlots = availablity.Where(r=> DateTime.Now.Day >= r.DateFrom.Day && DateTime.Now.Day <= r.DateFrom.Day).FirstOrDefault();

			

		}

		[TestMethod]
		public void TestDBContextAndDataSeedInitialisation1()
		{

			//_mockParkingService = new Mock<IParkingService>(_mockDBContext.Object);
			//_mockReservationRepository.Setup(s=>s.) = new Mock<ReservationRepository>();
			//_mockReservationRepository = new Mock<IReservationRepository>();
			//_mockReservationRepository.Setup(
			//	s => s.GetParkingSpaceAvailablity(DateTime.Now, DateTime.Now.AddDays(5)

			//	)).Returns;
			//var repContext = new ReservationRepository(_mockDBContext.Object);
			//var parkingService = new ParkingService(_mockReservationRepository.Object);

			//var availablity = parkingService.GetParkingSpaceAvailablity(DateTime.Now, DateTime.Now.AddDays(5));

			//Assert.IsNotNull(availablity);

			//var availableSlots = availablity.Where(r=> DateTime.Now.Day >= r.DateFrom.Day && DateTime.Now.Day <= r.DateFrom.Day).FirstOrDefault();



		}
		[TestMethod]
		public void BookReservation_ParkingService_Test1()
		{
			
			var repContext = new Mock<ReservationRepository>(_mockDBContext.Object);


			var _mockParkingService = new Mock<ParkingService>(repContext.Object);
			bool result = false;
			BookReservationRequest bookingRequest = new BookReservationRequest { DateFrom = DateTime.Now, DateTo = DateTime.Now, VehicleReg = "MB65TYG" };

			/* doesn't work
			_mockParkingService.Setup(
				s => s.BookReservation(bookingRequest

				)).Returns(result);

			*/
			
			Assert.AreEqual(true,result);



		}
		[TestMethod]
		public void NoReservations_GetAvaialbilty_AllSlotsFree_Test()
		{
			var mockContext = new Mock<ReservationDbContext>();
			//_mockDBContext.Setup(m => m.Reservations).Returns(mockSetReservation.Object);



			var parkingService = new ParkingService(_mockReservationRepository.Object);
			var availabilityResponses = parkingService.GetParkingSpaceAvailablity(DateTime.Now, DateTime.Now.AddDays(7));

			foreach (var availabilityResponse in availabilityResponses)
			{
				Assert.AreEqual(availabilityResponse.AvailableSpaces, 10);
			}

		}
		[TestMethod]
		public void Create_Reservation_Test()
		{


			var reservationData = new List<Reservation>().AsQueryable();

			var mockSetReservation = new Mock<DbSet<Reservation>>();
			mockSetReservation.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationData.Provider);
			mockSetReservation.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationData.Expression);
			mockSetReservation.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationData.ElementType);
			mockSetReservation.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(() => reservationData.GetEnumerator());

			_mockDBContext.Setup(c => c.Reservations).Returns(mockSetReservation.Object);

			ReservationRepository _resRepo = new ReservationRepository(_mockDBContext.Object);


			var parkingService = new ParkingService(_resRepo);
			BookReservationRequest bookingRequest = new BookReservationRequest { DateFrom = DateTime.Now, DateTo = DateTime.Now, VehicleReg = "MB65TYG" };
			parkingService.BookReservation(bookingRequest);


			var availabilityResponses = parkingService.GetParkingSpaceAvailablity(DateTime.Now, DateTime.Now);


			mockSetReservation.Verify(m => m.Add(It.IsAny<Reservation>()), Times.Once());
			_mockDBContext.Verify(m => m.SaveChanges(), Times.Once());

			Assert.AreEqual(availabilityResponses.First().AvailableSpaces, 9);


		}
		
		[TestMethod]
		public void createReservation_Save_Via_Context()
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

			mockSetReservation.Verify(m => m.Add(It.IsAny<Reservation>()), Times.Once());
			_mockDBContext.Verify(m => m.SaveChanges(), Times.Once());
			
		}
	}
}