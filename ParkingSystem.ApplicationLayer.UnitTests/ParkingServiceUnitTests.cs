using Microsoft.EntityFrameworkCore;
using ParkingSystem.ApplicationLayer.Services;
using ParkingSystem.Common.Requests;
using ParkingSystem.InfrastructureLayer.DbContextEF;
using ParkingSystem.InfrastructureLayer.Model;
using ParkingSystem.InfrastructureLayer.Repositories;

namespace ParkingSystem.ApplicationLayer.UnitTests
{
	[TestClass]
	public class ParkingServiceUnitTests
	{
		private ReservationDbContext _reservationDBContext;
		private ReservationRepository _reservationRepository;
		private ParkingService _parkingService;

		public ParkingServiceUnitTests()
		{

			InitialiseTestData();

		}

		private void InitialiseTestData()
		{

			var options = new DbContextOptionsBuilder<ReservationDbContext>()
			.UseInMemoryDatabase(databaseName: "ParkingSystemAPITestDB")
			.Options;

			_reservationDBContext = new ReservationDbContext(options);


			//prices by types
			if (_reservationDBContext.Prices.Count() == 0)
			{
				_reservationDBContext.Prices.Add(new Price { Id = 1, PriceType = "Summer", DailyCharge = 20, DateFrom = DateTime.Parse("2023-04-01"), DateTo = DateTime.Parse("2023-09-30") });
				_reservationDBContext.Prices.Add(new Price { Id = 2, PriceType = "Winter", DailyCharge = 30, DateFrom = DateTime.Parse("2022-10-01"), DateTo = DateTime.Parse("2023-03-31") });
			}
			//parking slots
			if (_reservationDBContext.ParkingSlots.Count() == 0)
			{
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 1, SlotName = "Slot1" });
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 2, SlotName = "Slot2" });
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 3, SlotName = "Slot3" });
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 4, SlotName = "Slot4" });
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 5, SlotName = "Slot5" });
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 6, SlotName = "Slot6" });
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 7, SlotName = "Slot7" });
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 8, SlotName = "Slot8" });
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 9, SlotName = "Slot9" });
				_reservationDBContext.ParkingSlots.Add(new ParkingSlot { SlotId = 10, SlotName = "Slot10" });
			}

			_reservationDBContext.SaveChanges();

			_reservationRepository = new ReservationRepository(_reservationDBContext);
			_parkingService = new ParkingService(_reservationRepository);

		}
		[TestMethod]
		public void Test_Initialisation()
		{
			AvailableReservationRequest request = new AvailableReservationRequest { DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(5) };
			var availablity = _parkingService.GetParkingSpaceAvailablity(request );

			Assert.IsNotNull(availablity);

		}


		[TestMethod]
		public void BookReservation_OneDay_9_RemainingSlotsAvailable()
		{

			BookReservationRequest bookingRequest = new BookReservationRequest { DateFrom = DateTime.Now, DateTo = DateTime.Now, VehicleReg = "MB65TYG" };
			var bookingResult = _parkingService.BookReservation(bookingRequest);

			Assert.IsTrue(bookingResult > 0);

			AvailableReservationRequest request = new AvailableReservationRequest { DateFrom = DateTime.Now, DateTo = DateTime.Now };

			var availability = _parkingService.GetParkingSpaceAvailablity(request).FirstOrDefault();

			Assert.IsNotNull(availability);
			Assert.AreEqual(9, availability.AvailableSpaces);

			//cleanup test data
			cleanUpReservation(bookingResult);
		}

		[TestMethod]
		public void NoReservations_GetAvaialbilty_AllSlotsFree_Test()
		{
			AvailableReservationRequest request = new AvailableReservationRequest { DateFrom = DateTime.Now, DateTo = DateTime.Now.AddDays(7) };

			var availabilityResponses = _parkingService.GetParkingSpaceAvailablity(request);

			foreach (var availabilityResponse in availabilityResponses)
			{
				Assert.AreEqual(availabilityResponse.AvailableSpaces, 10);
			}

		}
		[TestMethod]
		public void CancelReservation_Test()
		{

			BookReservationRequest bookingRequest = new BookReservationRequest { DateFrom = DateTime.Now, DateTo = DateTime.Now, VehicleReg = "ABCD76YTG" };

			var bookingResult = _parkingService.BookReservation(bookingRequest);

			Assert.IsTrue(bookingResult > 0);

			var bookedReservation = _reservationRepository.GetReservationById(bookingResult);

			CancelReservationRequest cancelRequest = new CancelReservationRequest { Id = bookingResult };

			var cancelResult = _reservationRepository.CancelReservation(cancelRequest);

			Assert.AreEqual(true, cancelResult.IsCancelled);

			cleanUpReservation(bookingResult);

		}
		[TestMethod]
		public void EditReservation_Test()
		{

			BookReservationRequest bookingRequest = new BookReservationRequest { DateFrom = DateTime.Now, DateTo = DateTime.Now, VehicleReg = "ABCD76YTG" };

			var bookingResult = _parkingService.BookReservation(bookingRequest);

			Assert.IsTrue(bookingResult > 0);

			var bookedReservation = _reservationRepository.GetReservationById(bookingResult);

			EditReservationRequest editRequest = new EditReservationRequest { Id = bookingResult };
			editRequest.BookingPrice = 50;
			var editResult = _reservationRepository.EditReservation(editRequest);

			Assert.AreEqual(editRequest.BookingPrice, editResult.BookingPrice);

			cleanUpReservation(bookingResult);
		}
		private void cleanUpReservation(int reservationId)
		{
			CancelReservationRequest cancelRequest = new CancelReservationRequest { Id = reservationId };

			_reservationRepository.CancelReservation(cancelRequest);

		}
	}
}