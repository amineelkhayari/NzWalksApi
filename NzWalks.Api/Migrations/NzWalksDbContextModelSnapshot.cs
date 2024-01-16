﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NzWalks.Api.Data;

#nullable disable

namespace NzWalks.Api.Migrations
{
    [DbContext(typeof(NzWalksDbContext))]
    partial class NzWalksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NzWalks.Api.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("19d7fabe-fb9c-4ed2-8a57-ddd6883d1cf9"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("61491422-c856-40a8-be39-628ced52b184"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("6507eea6-7583-4cbc-ac58-7b41399edee6"),
                            Name = "Hight"
                        });
                });

            modelBuilder.Entity("NzWalks.Api.Models.Domain.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtention")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("NzWalks.Api.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1fce25f9-534d-408a-9282-8a9e9d8a8d59"),
                            Code = "AKL",
                            Name = "Auckland",
                            RegionImageUrl = "#"
                        },
                        new
                        {
                            Id = new Guid("da88994d-d42a-49be-a560-a95126504ac7"),
                            Code = "NWZ",
                            Name = "NEW Zeeland",
                            RegionImageUrl = "#"
                        },
                        new
                        {
                            Id = new Guid("8e00c571-800d-46fb-ab2e-03dc6b0b24de"),
                            Code = "KECH",
                            Name = "MArrakech",
                            RegionImageUrl = "#"
                        },
                        new
                        {
                            Id = new Guid("ed172ea2-40ba-4f7d-a935-06dfaf92c0c8"),
                            Code = "AGR",
                            Name = "Agadir",
                            RegionImageUrl = "#"
                        },
                        new
                        {
                            Id = new Guid("0898bf46-9742-4b0c-b8bf-773e2c5f7740"),
                            Code = "CSA",
                            Name = "CASA",
                            RegionImageUrl = "#"
                        });
                });

            modelBuilder.Entity("NzWalks.Api.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LenghtInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NzWalks.Api.Models.Domain.Walk", b =>
                {
                    b.HasOne("NzWalks.Api.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NzWalks.Api.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}