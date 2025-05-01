using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TollUsage> TollUsages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TollUsage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.TollStation).HasMaxLength(100).IsRequired();
                entity.Property(e => e.City).HasMaxLength(100).IsRequired();
                entity.Property(e => e.State).HasMaxLength(2).IsRequired();
                entity.Property(e => e.AmountPaid).HasColumnType("decimal(10,2)");
                entity.Property(e => e.VehicleType).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}