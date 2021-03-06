﻿// <auto-generated />
using System;
using CustomerService.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerService.Migrations
{
    [DbContext(typeof(CustomerContext))]
    [Migration("20201008174632_InitialCreateCustomerService")]
    partial class InitialCreateCustomerService
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomerService.Models.CreditReservation", b =>
                {
                    b.Property<long>("OrderId")
                        .HasColumnName("orderId")
                        .HasColumnType("bigint");

                    b.Property<long>("CustomerId")
                        .HasColumnName("customerId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CreditReservations");
                });

            modelBuilder.Entity("CustomerService.Models.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnName("creationtime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("CustomerService.Models.CreditReservation", b =>
                {
                    b.HasOne("CustomerService.Models.Customer", null)
                        .WithMany("CreditReservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ServiceCommon.Classes.Money", "OrderTotal", b1 =>
                        {
                            b1.Property<long>("CreditReservationOrderId")
                                .HasColumnType("bigint");

                            b1.Property<decimal>("Amount")
                                .HasColumnName("amount")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("CreditReservationOrderId");

                            b1.ToTable("CreditReservations");

                            b1.WithOwner()
                                .HasForeignKey("CreditReservationOrderId");
                        });
                });

            modelBuilder.Entity("CustomerService.Models.Customer", b =>
                {
                    b.OwnsOne("ServiceCommon.Classes.Money", "CreditLimit", b1 =>
                        {
                            b1.Property<long>("CustomerId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Amount")
                                .HasColumnName("amount")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
