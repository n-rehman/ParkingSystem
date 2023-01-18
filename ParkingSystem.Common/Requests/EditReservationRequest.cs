namespace ParkingSystem.Common.Requests
{
	public class EditReservationRequest
	{
		public int Id { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public double BookingPrice { get; set; }
	}
}
