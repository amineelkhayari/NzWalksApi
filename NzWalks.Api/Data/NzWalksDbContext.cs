using Microsoft.EntityFrameworkCore;
using NzWalks.Api.Models.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NzWalks.Api.Data
{
    public class NzWalksDbContext:DbContext
    {
        public NzWalksDbContext(DbContextOptions<NzWalksDbContext> dbContextOptions):base(dbContextOptions)
        {



        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //seed data for defeculties
            //easy,Medium,Hard
            var difficulties = new List<Difficulty>()
            {
                 new Difficulty()
                 {
                     Id=Guid.Parse("19d7fabe-fb9c-4ed2-8a57-ddd6883d1cf9"),
                     Name="Easy"
                 },
                 new Difficulty()
                 {
                     Id=Guid.Parse("61491422-c856-40a8-be39-628ced52b184"),
                     Name="Medium"
                 },
                 new Difficulty()
                 {
                     Id=Guid.Parse("6507eea6-7583-4cbc-ac58-7b41399edee6"),
                     Name="Hight"
                 }
            };
           
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
            {
                new Region
                {
                    Id=Guid.Parse("1fce25f9-534d-408a-9282-8a9e9d8a8d59"),
                    Code="AKL",
                    Name="Auckland",
                    RegionImageUrl="#"
                },
                new Region
                {
                    Id=Guid.Parse("da88994d-d42a-49be-a560-a95126504ac7"),
                    Code="NWZ",
                    Name="NEW Zeeland",
                    RegionImageUrl="#"
                },
                new Region
                {
                    Id=Guid.Parse("8e00c571-800d-46fb-ab2e-03dc6b0b24de"),
                    Code="KECH",
                    Name="MArrakech",
                    RegionImageUrl="#"
                },
                new Region
                {
                    Id=Guid.Parse("ed172ea2-40ba-4f7d-a935-06dfaf92c0c8"),
                    Code="AGR",
                    Name="Agadir",
                    RegionImageUrl="#"
                },
                new Region
                {
                    Id=Guid.Parse("0898bf46-9742-4b0c-b8bf-773e2c5f7740"),
                    Code="CSA",
                    Name="CASA",
                    RegionImageUrl="#"
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }

    } 
}
