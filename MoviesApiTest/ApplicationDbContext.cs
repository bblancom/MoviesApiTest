using GrowthApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrowthApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().Property(p => p.Name).HasMaxLength(50);
        }

        public DbSet<Genre> Genres { get; set; }
    }
}
