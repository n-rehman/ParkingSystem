namespace ParkingSystem.InfrastructureLayer.Model
{
    public class Price
    {
        public int Id { get; set; }
        public string PriceType { get; set; } //really be enum or id from other table but keeping simple for test
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double DailyCharge { get; set; }

    }
}
