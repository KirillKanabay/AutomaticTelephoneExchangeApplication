using ATE.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATE.Infrastructure.Data.Config
{
    class ClientConfiguration:IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.Id).IsRequired();
            builder.Property(pb => pb.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(pb => pb.SecondName).IsRequired().HasMaxLength(128);

            builder.HasMany(c => c.Contracts).WithOne().HasForeignKey(c => c.ClientId);
        }
    }
}
