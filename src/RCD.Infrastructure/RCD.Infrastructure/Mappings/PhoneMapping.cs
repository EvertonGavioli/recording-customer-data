using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCD.Domain.Models;

namespace RCD.Infrastructure.Mappings
{
    public class PhoneMapping : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PhoneType)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .IsRequired();

            builder.ToTable("Phones");
        }
    }
}
