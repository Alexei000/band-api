using System;
using BandApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BandApi.DbContexts
{
    public class BandAlbumContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }

        public BandAlbumContext(DbContextOptions<BandAlbumContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Band>().HasData(
                new Band
                {
                    Id = Guid.Parse("423d5dc6-64ba-4556-9032-88c9ad246eba"),
                    Name = "Metallica",
                    Founded = new DateTime(1980, 1, 1),
                    MainGenre = "Heavy Metal"
                },
                new Band
                {
                    Id = Guid.Parse("c75c4a5c-890e-491e-85de-933ea2ab25ab"),
                    Name = "Guns N Roses",
                    Founded = new DateTime(1985, 3, 1),
                    MainGenre = "Rock"
                },
                new Band
                {
                    Id = Guid.Parse("806b7c4e-242b-456d-bcf2-e50b9be83393"),
                    Name = "ABBA",
                    Founded = new DateTime(1965, 7, 1),
                    MainGenre = "Disco"
                },
                new Band
                {
                    Id = Guid.Parse("6de68de3-0e30-4756-8070-6747506631f6"),
                    Name = "Oasis",
                    Founded = new DateTime(1991, 12, 1),
                    MainGenre = "Alternative"
                },
                new Band
                {
                    Id = Guid.Parse("0673514e-80ec-4a4a-9b86-fd067efe9217"),
                    Name = "Oasis",
                    Founded = new DateTime(1991, 12, 1),
                    MainGenre = "Alternative"
                },
                new Band
                {
                    Id = Guid.Parse("2bed73ff-2990-4302-90d2-b0be7d45db4e"),
                    Name = "A-ha",
                    Founded = new DateTime(1981, 6, 1),
                    MainGenre = "Pop"
                }
            );

            modelBuilder.Entity<Album>().HasData(
                new Album
                {
                    Id = Guid.Parse("b4425cfd-b6a1-4708-a521-17060d6d0d7d"),
                    Title = "Master of Puppets",
                    Description = "Best heavy metal albums",
                    BandId = Guid.Parse("423d5dc6-64ba-4556-9032-88c9ad246eba"),
                },
                new Album
                {
                    Id = Guid.Parse("004c1ca2-aa10-442c-970d-20ef26542994"),
                    Title = "Appetite for Destruction",
                    Description = "Amazing Rock Album",
                    BandId = Guid.Parse("c75c4a5c-890e-491e-85de-933ea2ab25ab"),
                },
                new Album
                {
                    Id = Guid.Parse("f29e9274-1e56-48d0-93a9-2c1f1a95eb0a"),
                    Title = "Waterloo",
                    Description = "Very Groovy Album",
                    BandId = Guid.Parse("806b7c4e-242b-456d-bcf2-e50b9be83393"),
                },
                new Album
                {
                    Id = Guid.Parse("de00294c-54dc-41c4-b5f7-ccc886e7b0b2"),
                    Title = "Be Here Now",
                    Description = "Oasis best",
                    BandId = Guid.Parse("0673514e-80ec-4a4a-9b86-fd067efe9217"),
                },
                new Album
                {
                    Id = Guid.Parse("119e5ac0-6bcb-4e0c-9daa-0ac10c2ea4c6"),
                    Title = "Hunting High and Low",
                    Description = "Debut Album",
                    BandId = Guid.Parse("2bed73ff-2990-4302-90d2-b0be7d45db4e")
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
