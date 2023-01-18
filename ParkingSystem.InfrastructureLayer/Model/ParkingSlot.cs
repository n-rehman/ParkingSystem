namespace ParkingSystem.InfrastructureLayer.Model
{
    public class ParkingSlot
    {
        public int SlotId { get; set; }
        public string SlotName { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
    }
}
