namespace CarRentalSystem.Infrastructure.Persistence
{
    using System.Reflection;
    using Domain.Models.CarAds;
    using Domain.Models.Dealers;
    using Microsoft.EntityFrameworkCore;

    internal class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options)
        {
        }

        public DbSet<CarAd> CarAds { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

        public DbSet<Dealer> Dealers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
