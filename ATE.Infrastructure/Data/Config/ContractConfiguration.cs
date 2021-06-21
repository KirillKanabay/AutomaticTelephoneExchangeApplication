using ATE.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATE.Infrastructure.Data.Config
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.ClientId).IsRequired();
            builder.Property(c => c.TariffId).IsRequired();
            builder.Property(c => c.PhoneNumber).IsRequired();

            builder.HasOne(c => c.Company).WithMany(c => c.Contracts);
            builder.HasOne(c => c.Client).WithMany(c => c.Contracts).HasForeignKey(c => c.ClientId);
            builder.HasOne(c => c.Tariff).WithMany().HasForeignKey(c => c.TariffId);
        }
    }
}
