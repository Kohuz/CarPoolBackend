using CarPool.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CarPool.DAL.Context
{
    public class CarPoolDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Ride> Rides { get; set; }


        public CarPoolDbContext(DbContextOptions<CarPoolDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.RidesTaking)
                .WithMany(e => e.Passengers);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.RidesOffering)
                .WithOne(e => e.Driver)
                .HasForeignKey(e => e.DriverId)
                .IsRequired();
        }
    }
}