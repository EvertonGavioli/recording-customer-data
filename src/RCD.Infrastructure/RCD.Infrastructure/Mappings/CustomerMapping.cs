using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RCD.Domain.Models;
using System.Collections.Generic;

namespace RCD.Infrastructure.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.OwnsOne(p => p.Email, tf =>
            {
                tf.Property(p => p.EmailAddress)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType("varchar(100)");
            });

            builder.HasMany(p => p.Phones)
                .WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomersPhones",
                    p => p.HasOne<Phone>().WithMany().HasForeignKey("PhoneId"),
                    p => p.HasOne<Customer>().WithMany().HasForeignKey("CustomerId")
                );

            builder.HasMany(p => p.Addresses)
                .WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomersAddresses",
                    p => p.HasOne<Address>().WithMany().HasForeignKey("AddressId"),
                    p => p.HasOne<Customer>().WithMany().HasForeignKey("CustomerId")
                );

            builder.ToTable("Customers");
        }
    }
}
