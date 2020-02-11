﻿// <auto-generated />
using System;
using BandApi.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BandApi.Migrations
{
    [DbContext(typeof(BandAlbumContext))]
    [Migration("20200211145814_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BandApi.Entities.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("BandId");

                    b.ToTable("Albums");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b4425cfd-b6a1-4708-a521-17060d6d0d7d"),
                            BandId = new Guid("423d5dc6-64ba-4556-9032-88c9ad246eba"),
                            Description = "Best heavy metal albums",
                            Title = "Master of Puppets"
                        },
                        new
                        {
                            Id = new Guid("004c1ca2-aa10-442c-970d-20ef26542994"),
                            BandId = new Guid("c75c4a5c-890e-491e-85de-933ea2ab25ab"),
                            Description = "Amazing Rock Album",
                            Title = "Appetite for Destruction"
                        },
                        new
                        {
                            Id = new Guid("f29e9274-1e56-48d0-93a9-2c1f1a95eb0a"),
                            BandId = new Guid("806b7c4e-242b-456d-bcf2-e50b9be83393"),
                            Description = "Very Groovy Album",
                            Title = "Waterloo"
                        },
                        new
                        {
                            Id = new Guid("de00294c-54dc-41c4-b5f7-ccc886e7b0b2"),
                            BandId = new Guid("0673514e-80ec-4a4a-9b86-fd067efe9217"),
                            Description = "Oasis best",
                            Title = "Be Here Now"
                        },
                        new
                        {
                            Id = new Guid("119e5ac0-6bcb-4e0c-9daa-0ac10c2ea4c6"),
                            BandId = new Guid("2bed73ff-2990-4302-90d2-b0be7d45db4e"),
                            Description = "Debut Album",
                            Title = "Hunting High and Low"
                        });
                });

            modelBuilder.Entity("BandApi.Entities.Band", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Founded")
                        .HasColumnType("datetime2");

                    b.Property<string>("MainGenre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Bands");

                    b.HasData(
                        new
                        {
                            Id = new Guid("423d5dc6-64ba-4556-9032-88c9ad246eba"),
                            Founded = new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MainGenre = "Heavy Metal",
                            Name = "Metallica"
                        },
                        new
                        {
                            Id = new Guid("c75c4a5c-890e-491e-85de-933ea2ab25ab"),
                            Founded = new DateTime(1985, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MainGenre = "Rock",
                            Name = "Guns N Roses"
                        },
                        new
                        {
                            Id = new Guid("806b7c4e-242b-456d-bcf2-e50b9be83393"),
                            Founded = new DateTime(1965, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MainGenre = "Disco",
                            Name = "ABBA"
                        },
                        new
                        {
                            Id = new Guid("6de68de3-0e30-4756-8070-6747506631f6"),
                            Founded = new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MainGenre = "Alternative",
                            Name = "Oasis"
                        },
                        new
                        {
                            Id = new Guid("0673514e-80ec-4a4a-9b86-fd067efe9217"),
                            Founded = new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MainGenre = "Alternative",
                            Name = "Oasis"
                        },
                        new
                        {
                            Id = new Guid("2bed73ff-2990-4302-90d2-b0be7d45db4e"),
                            Founded = new DateTime(1981, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MainGenre = "Pop",
                            Name = "A-ha"
                        });
                });

            modelBuilder.Entity("BandApi.Entities.Album", b =>
                {
                    b.HasOne("BandApi.Entities.Band", "Band")
                        .WithMany("Type")
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}