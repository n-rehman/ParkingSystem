using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ParkingSystem.ApplicationLayer.Interfaces;
using ParkingSystem.Common.Requests;
using ParkingSystem.Common.Responses;
using ParkingSystem.InfrastructureLayer.DbContextEF;
using ParkingSystem.InfrastructureLayer.Model;
using ParkingSystem.InfrastructureLayer.Repositories;

namespace ParkingSystem.ApplicationLayer.Services
{
	public class ParkingService : IParkingService
	{
		private readonly IReservationRepository _reservationRepo;

		public ParkingService(IReservationRepository reservationDbContext)
		{
			_reservationRepo = reservationDbContext;
		}

		public int EditReservation(EditReservationRequest editReservationRequest)
		{

			int editedReservationId = 0;
			var availability = GetParkingSpaceAvailablity(editReservationRequest.DateFrom, editReservationRequest.DateTo);
			if (availability.Count >= 1)
			{
				var price = GetParkingCharge(editReservationRequest.DateFrom, editReservationRequest.DateTo);


				editReservationRequest.BookingPrice = price;
				var editedReservation = _reservationRepo.EditReservation(editReservationRequest);
				editedReservationId = editedReservation.Id;

			}

			return editedReservationId;

		}

		public int BookReservation(BookReservationRequest bookReservationRequest)
		{
			int bookedReservationId = 0;
			var availability = GetParkingSpaceAvailablity(bookReservationRequest.DateFrom, bookReservationRequest.DateTo);
			var price = GetParkingCharge(bookReservationRequest.DateFrom, bookReservationRequest.DateTo);
			if (availability.Count >= 1)
			{
				bookReservationRequest.BookingPrice = price;
				bookReservationRequest.ParkingSlotNo = availability.First().AvailableSlotIds.First();
				var bookedReservation = _reservationRepo.BookReservation(bookReservationRequest);

				bookedReservationId = bookedReservation.Id;
			}

			return bookedReservationId;
		}

		public int CancelReservation(CancelReservationRequest cancelReservationRequest)
		{

			var cancelledReservation = _reservationRepo.CancelReservation(cancelReservationRequest);
			return cancelledReservation.Id;

		}

		public List<ReservationAvailabilityResponse> GetParkingSpaceAvailablity(AvailableReservationRequest availableReservationRequest)
		{
			
			return GetParkingSpaceAvailablity(availableReservationRequest.DateFrom, availableReservationRequest.DateTo);


		}


		public double GetParkingCharge(DateTime dateFrom, DateTime dateTo)
		{
			//workout price to use as per dates range
			double totalPrice = 0;

			var days = GetNumberOfDays(dateFrom, dateTo);

			var dateToCheck = dateFrom;
			//required to check price per day as 2 dates can fall in different price types date range
			for (int i = 1; i <= days; i++)
			{
				totalPrice += _reservationRepo.GetAllPrices().Where(pr => dateToCheck >= pr.DateFrom && dateToCheck <= pr.DateTo).Select(t => t.DailyCharge).FirstOrDefault();
				dateToCheck = dateToCheck.AddDays(1);
			}


			return totalPrice;
		}
		private int GetNumberOfDays(DateTime dateFrom, DateTime dateTo)
		{
			//workout price to use as per dates range
			TimeSpan diffResult = dateTo - dateFrom;
			var days = (int)Math.Abs(diffResult.TotalDays) + 1;//+1 required to include starting date

			return days;
		}
		private List<ReservationAvailabilityResponse> GetParkingSpaceAvailablity(DateTime dateFrom, DateTime dateTo)
		{

			var allReservations = _reservationRepo.GetAllReservationsIncludingParkingSlots();

			var priceForDatesSearched = GetParkingCharge(dateFrom, dateTo);
			//work-out no of days to show for availablity
			var days = GetNumberOfDays(dateFrom, dateTo);

			List<ReservationAvailabilityResponse> availableSlotsResult = new List<ReservationAvailabilityResponse>();
			var availablityDate = dateFrom;
			for (int i = 1; i <= days; i++)
			{
				var row = new ReservationAvailabilityResponse();

				row.DateFrom = availablityDate;
				row.DateTo = dateTo;
				int bookedSlotsForDay = allReservations.Count(r => availablityDate.Day >= r.DateFrom.Day && availablityDate.Day <= r.DateTo.Day && r.IsCancelled == false);
				row.AvailableSpaces = _reservationRepo.GetAllParkingSlots().Count() - bookedSlotsForDay;
				row.AvailableSlotIds = _reservationRepo.GetAllParkingSlots().Select(a => a.SlotId).Except(allReservations.Where(r => availablityDate.Day >= r.DateFrom.Day && availablityDate.Day <= r.DateTo.Day && r.IsCancelled == false).Select(r => r.ParkingSlots.SlotId)).ToList<int>();
				var pricePerDay = GetParkingCharge(availablityDate, availablityDate);
				row.DailyParkingCharge = pricePerDay;
				row.TotalParkingCharge = priceForDatesSearched;
				availableSlotsResult.Add(row);

				availablityDate = availablityDate.AddDays(1);
			}

			return availableSlotsResult;
		}
	}
}
