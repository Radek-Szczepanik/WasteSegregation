﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WasteSegregation.Infrastructure.Data;

#nullable disable

namespace WasteSegregation.WebAPI.Migrations
{
    [DbContext(typeof(WasteSegregationDbContext))]
    partial class WasteSegregationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WasteSegregation.Domain.Entities.RealEstate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RealEstates");
                });

            modelBuilder.Entity("WasteSegregation.Domain.Entities.RealEstateWaste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte?>("BlueBag")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("BrownBag")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("GreenBag")
                        .HasColumnType("tinyint");

                    b.Property<int>("RealEstateId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReceiptDate")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("YellowBag")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("RealEstateId");

                    b.ToTable("RealEstateWastes");
                });

            modelBuilder.Entity("WasteSegregation.Domain.Entities.RealEstateWaste", b =>
                {
                    b.HasOne("WasteSegregation.Domain.Entities.RealEstate", "RealEstate")
                        .WithMany("RealEstateWastes")
                        .HasForeignKey("RealEstateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealEstate");
                });

            modelBuilder.Entity("WasteSegregation.Domain.Entities.RealEstate", b =>
                {
                    b.Navigation("RealEstateWastes");
                });
#pragma warning restore 612, 618
        }
    }
}
