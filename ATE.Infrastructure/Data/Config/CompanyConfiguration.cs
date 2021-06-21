using ATE.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATE.Infrastructure.Data.Config
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.CountryCode).IsRequired().HasMaxLength(5);
            builder.Property(c => c.CompanyCode).IsRequired().HasMaxLength(5);
        }
    }
}
