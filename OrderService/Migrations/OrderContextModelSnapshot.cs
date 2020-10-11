﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderService.DBContext;

namespace OrderService.Migrations
{
    [DbContext(typeof(OrderContext))]
    partial class OrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderService.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnName("state")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Version")
                        .HasColumnName("version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("OrderService.Models.Order", b =>
                {
                    b.OwnsOne("ServiceCommon.Classes.OrderDetails", "OrderDetails", b1 =>
                        {
                            b1.Property<long>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<long>("CustomerId")
                                .HasColumnName("customerId")
                                .HasColumnType("bigint");

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsOne("ServiceCommon.Classes.Money", "OrderTotal", b2 =>
                                {
                                    b2.Property<long>("OrderDetailsOrderId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("bigint")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<decimal>("Amount")
                                        .HasColumnName("amount")
                                        .HasColumnType("decimal(18,2)");

                                    b2.HasKey("OrderDetailsOrderId");

                                    b2.ToTable("Order");

                                    b2.WithOwner()
                                        .HasForeignKey("OrderDetailsOrderId");
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}