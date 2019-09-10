using System.Data.Entity;

namespace RentAndCycleCodeFirst.Models
{  
    public class BikeDbContext : DbContext
    {
        public BikeDbContext() : base("BikeDbContext")
        {
        }
        
        public virtual DbSet<Bike> Bikes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyBike> CompanyBikes { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CompanyLocation> CompanyLocations { get; set; }
    }
}
