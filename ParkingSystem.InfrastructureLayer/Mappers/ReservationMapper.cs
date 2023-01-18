using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSystem.InfrastructureLayer.Model;

namespace ParkingSystem.InfrastructureLayer.Mappers
{
	public class ReservationMapper
	{
		public ReservationMapper(EntityTypeBuilder<Reservation> entityTypeBuilder)
		{
			entityTypeBuilder.HasKey(x => x.Id);
			entityTypeBuilder.Property(x => x.SlotId);
			entityTypeBuilder.Property(x => x.DateFrom).IsRequired();
			entityTypeBuilder.Property(x => x.DateTo).IsRequired();
			entityTypeBuilder.Property(x => x.VehicleReg).IsRequired();
			entityTypeBuilder.Property(x => x.BookingPrice).IsRequired();
			entityTypeBuilder.Property(x => x.IsCancelled);
			entityTypeBuilder.HasOne(x => x.ParkingSlots).WithMany(r => r.Reservations).HasForeignKey(x => x.SlotId);
		}
	}
}
