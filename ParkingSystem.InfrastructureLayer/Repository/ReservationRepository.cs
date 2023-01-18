using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ParkingSystem.Common.Requests;
using ParkingSystem.Common.Responses;
using ParkingSystem.InfrastructureLayer.DbContextEF;
using ParkingSystem.InfrastructureLayer.Model;
using System.Collections.Generic;

namespace ParkingSystem.InfrastructureLayer.Repositories
{
	public class ReservationRepository : IReservationRepository
	{
		private readonly ReservationDbContext _reservationDbContext;

		public ReservationRepository(ReservationDbContext reservationDbContext)
		{
			_reservationDbContext = reservationDbContext;
		}

		public Reservation EditReservation(EditReservationRequest editReservationRequest)
		{
			var reservationDetails = GetReservationById(editReservationRequest.Id);


			reservationDetails.BookingPrice = editReservationRequest.BookingPrice;
			reservationDetails.DateFrom = editReservationRequest.DateFrom;
			reservationDetails.DateTo = editReservationRequest.DateTo;

			_reservationDbContext.Update(reservationDetails);
			int result = _reservationDbContext.SaveChanges();

			return reservationDetails;
		}

		public Reservation BookReservation(BookReservationRequest bookReservationObj)
		{
			var reservationToAdd = new Reservation
			{
				DateFrom = bookReservationObj.DateFrom,
				DateTo = bookReservationObj.DateTo,
				BookingPrice = bookReservationObj.BookingPrice,
				SlotId = bookReservationObj.ParkingSlotNo,
				VehicleReg = bookReservationObj.VehicleReg,
				IsCancelled = false,

			};

			_reservationDbContext.Reservations.Add(reservationToAdd);

			var rowsAffected = _reservationDbContext.SaveChanges();

			return rowsAffected == 1 ? reservationToAdd : null;
		}

		public Reservation CancelReservation(CancelReservationRequest cancelReservationObj)
		{
			var reservationDetails = GetReservationById(cancelReservationObj.Id);
			reservationDetails.IsCancelled = true;
			_reservationDbContext.Update(reservationDetails);
			int result = _reservationDbContext.SaveChanges();

			return reservationDetails;
		}
		
		public Reservation GetReservationById(int reservationID)
		{
			return _reservationDbContext.Reservations.Where(r => r.Id == reservationID).First();
		}

		public IEnumerable<Reservation> GetAllReservations()
		{
			if (_reservationDbContext.Reservations == null)
				return new List<Reservation>();

			return _reservationDbContext.Reservations.Select(r => r);//.ToList();
		}
		public IIncludableQueryable<Reservation, ParkingSlot> GetAllReservationsIncludingParkingSlots()
		{
			return _reservationDbContext.Reservations.Include(r => r.ParkingSlots);
		}
		public List<ParkingSlot> GetAllParkingSlots()
		{
			return _reservationDbContext.ParkingSlots.ToList();
		}

		public List<Price> GetAllPrices()
		{
			return _reservationDbContext.Prices.ToList();
		}


	}
}