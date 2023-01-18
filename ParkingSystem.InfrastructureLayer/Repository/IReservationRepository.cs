
using Microsoft.EntityFrameworkCore.Query;
using ParkingSystem.Common.Requests;
using ParkingSystem.Common.Responses;
using ParkingSystem.InfrastructureLayer.Model;

namespace ParkingSystem.InfrastructureLayer.Repositories
{
	public interface IReservationRepository
	{
		Reservation BookReservation(BookReservationRequest bookReservationRequest);
		Reservation CancelReservation(CancelReservationRequest cancelReservationRequest);
		Reservation EditReservation(EditReservationRequest editReservationRequest);
		Reservation GetReservationById(int reservationID);
		IEnumerable<Reservation> GetAllReservations();
		IIncludableQueryable<Reservation, ParkingSlot> GetAllReservationsIncludingParkingSlots();
		List<ParkingSlot> GetAllParkingSlots();
		List<Price> GetAllPrices();

	}
}