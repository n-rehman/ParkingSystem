using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingSystem.InfrastructureLayer.Model
{
    public class Reservation
    {
		//[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public int Id { get; set; }
        public string? VehicleReg { get; set; }
        public int SlotId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double BookingPrice { get; set; }
        public bool IsCancelled { get; set; }
        public virtual ParkingSlot ParkingSlots { get; set; }
    }
}
