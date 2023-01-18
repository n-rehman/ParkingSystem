namespace ParkingSystem.Common.Requests
{
	public class BookReservationRequest
	{
		public string? VehicleReg { get; set; }
		public int ParkingSlotNo { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public double BookingPrice { get; set; }
	}
}
