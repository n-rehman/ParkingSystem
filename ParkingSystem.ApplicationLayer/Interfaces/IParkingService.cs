using ParkingSystem.Common.Requests;
using ParkingSystem.Common.Responses;

namespace ParkingSystem.ApplicationLayer.Interfaces
{
    public interface IParkingService
	{
		int EditReservation(EditReservationRequest reservationEditDto);
		int BookReservation(BookReservationRequest reservationBookDto);
		int CancelReservation(CancelReservationRequest cancelReservationDto);
		List<ReservationAvailabilityResponse> GetParkingSpaceAvailablity(AvailableReservationRequest availableReservationRequest);
		double GetParkingCharge(DateTime dateFrom, DateTime dateTo);
	}
}