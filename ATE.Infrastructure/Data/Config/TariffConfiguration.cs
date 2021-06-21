using ATE.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATE.Infrastructure.Data.Config
{
    public class TariffConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            builder.Property(t => t.Id).IsRequired();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(128);
            builder.Property(t => t.PricePerMinuteCall).IsRequired();
        }
    }
}