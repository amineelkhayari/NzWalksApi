using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NzWalks.Api.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "c02502c9-ddef-48cf-b199-57cc41e6110a";
            var WriterRoleId = "9e2b6b15-d2da-4cfd-9b82-d4dd466c8174";
            var roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                    
                },
                new IdentityRole{
                    Id=WriterRoleId,
                    ConcurrencyStamp=WriterRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()

                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

