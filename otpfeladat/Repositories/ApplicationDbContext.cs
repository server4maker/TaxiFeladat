using Microsoft.EntityFrameworkCore;
using otpfeladat.Models;

namespace otpfeladat.repositories
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = 1, PassengerCapacity = 4, Range = 250, Fuel = FuelType.Gasoline },
                new Vehicle { Id = 2, PassengerCapacity = 3, Range = 120, Fuel = FuelType.MildHybrid },
                new Vehicle { Id = 3, PassengerCapacity = 2, Range = 100, Fuel = FuelType.PureElectric }
            );
        }
    }
}
