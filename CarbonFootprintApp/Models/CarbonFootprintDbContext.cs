using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarbonFootprintApp.Models
{
    public class CarbonFootprintDbContext : IdentityDbContext<ApplicationUser>
    {
        public CarbonFootprintDbContext(DbContextOptions<CarbonFootprintDbContext> options) : base(options)
        {

        }

        public DbSet<FootprintHistory> FootprintHistories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}