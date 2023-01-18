using ParkingSystem.InfrastructureLayer.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkingSystem.InfrastructureLayer.Mappers
{
    public class PriceMapper
    {
        public PriceMapper(EntityTypeBuilder<Price> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.DailyCharge);
            entityTypeBuilder.Property(x => x.PriceType).IsRequired();
        }
    }
}
