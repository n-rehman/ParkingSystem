using ParkingSystem.InfrastructureLayer.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkingSystem.InfrastructureLayer.Mappers
{
	public class ParkingSlotMapper
	{
		public ParkingSlotMapper(EntityTypeBuilder<ParkingSlot> entityTypeBuilder)
		{
			entityTypeBuilder.HasKey(x => x.SlotId);
			entityTypeBuilder.Property(x => x.SlotName);
		}
	}
}
