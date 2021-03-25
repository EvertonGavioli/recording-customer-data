using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCD.Domain.Models;

namespace RCD.Infrastructure.Mappings
{
    public class AddressMaping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Street)
                .IsRequired();

            builder.Property(p => p.Number)
                .IsRequired();

            builder.Property(p => p.ZipCode)
                .IsRequired();

            builder.Property(p => p.City)
                .IsRequired();

            builder.Property(p => p.State)
                .IsRequired();

            builder.Property(p => p.Country)
                .IsRequired();

            builder.ToTable("Addresses");
        }
    }
}
