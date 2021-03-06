// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RCD.Infrastructure.Data;

namespace RCD.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210324205529_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomersAddresses", b =>
                {
                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AddressId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomersAddresses");
                });

            modelBuilder.Entity("CustomersPhones", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PhoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomerId", "PhoneId");

                    b.HasIndex("PhoneId");

                    b.ToTable("CustomersPhones");
                });

            modelBuilder.Entity("RCD.Domain.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("RCD.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("RCD.Domain.Models.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("PhoneType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("CustomersAddresses", b =>
                {
                    b.HasOne("RCD.Domain.Models.Address", null)
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RCD.Domain.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CustomersPhones", b =>
                {
                    b.HasOne("RCD.Domain.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RCD.Domain.Models.Phone", null)
                        .WithMany()
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RCD.Domain.Models.Customer", b =>
                {
                    b.OwnsOne("RCD.Core.DomainObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("EmailAddress")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Email");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Email");
                });
#pragma warning restore 612, 618
        }
    }
}
