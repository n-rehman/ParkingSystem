namespace ParkingSystem.Common.Responses
{
    public class ReservationAvailabilityResponse
    {
        public int AvailableSpaces { get; set; }
        public List<int> AvailableSlotIds { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double? TotalParkingCharge { get; set; }
        public double? DailyParkingCharge { get; set; }
    }
}
