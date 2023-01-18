using Microsoft.EntityFrameworkCore;
using ParkingSystem.InfrastructureLayer.Mappers;
using ParkingSystem.InfrastructureLayer.Model;

namespace ParkingSystem.InfrastructureLayer.DbContextEF
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext()
        {
			//Reservations = Set<Reservation>();
		}

        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
        {
			//Reservations = Set<Reservation>();
		}

        public DbSet<Reservation> Reservations { set; get; }
        public DbSet<ParkingSlot> ParkingSlots { set; get; }
        public DbSet<Price> Prices { set; get; }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //	if (!optionsBuilder.IsConfigured)
        //	{
        //	}
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ReservationMapper(modelBuilder.Entity<Reservation>());
            new ParkingSlotMapper(modelBuilder.Entity<ParkingSlot>());
            new PriceMapper(modelBuilder.Entity<Price>());
        }
    }
}
