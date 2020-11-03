﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZPastel.Persistence;

namespace ZPastel.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201103122229_Initial_Migration")]
    partial class Initial_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZPastel.Model.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedById")
                        .HasColumnName("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedByUsername")
                        .HasColumnName("CreatedByUsername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("LastModifiedById")
                        .HasColumnName("LastModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnName("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnName("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ZPastel.Model.OrderItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ingredients")
                        .HasColumnName("Ingredients")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("LastModifiedById")
                        .HasColumnName("LastModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnName("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderItemId")
                        .HasColumnType("bigint");

                    b.Property<long>("PastelId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnName("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("OrderId");

                    b.HasIndex("OrderItemId");

                    b.HasIndex("PastelId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("ZPastel.Model.Pastel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedById")
                        .HasColumnName("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ingredients")
                        .HasColumnName("Ingredients")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnName("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<long>("LastModifiedById")
                        .HasColumnName("LastModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModifiedOn")
                        .HasColumnName("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PasteleiroId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("PasteleiroId");

                    b.ToTable("Pastel");
                });

            modelBuilder.Entity("ZPastel.Model.Pasteleiro", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pasteleiro");
                });

            modelBuilder.Entity("ZPastel.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ZPastel.Model.Order", b =>
                {
                    b.HasOne("ZPastel.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ZPastel.Model.OrderItem", b =>
                {
                    b.HasOne("ZPastel.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZPastel.Model.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ZPastel.Model.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ZPastel.Model.Pastel", "Pastel")
                        .WithMany()
                        .HasForeignKey("PastelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ZPastel.Model.Pastel", b =>
                {
                    b.HasOne("ZPastel.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZPastel.Model.Pasteleiro", "Pasteleiro")
                        .WithMany()
                        .HasForeignKey("PasteleiroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
