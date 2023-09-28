using CarPool.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.DAL.Context
{
    public class CarPoolDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Ride> Rides => Set<Ride>();


        public CarPoolDbContext(DbContextOptions<CarPoolDbContext> options)
            : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.RidesTaking)
                .WithMany(e => e.Passengers);

            modelBuilder.Entity<User>()
                .HasMany(e => e.RidesOffering)
                .WithOne(e => e.Driver)
                .HasForeignKey(e => e.DriverId)
                .IsRequired();
        }
    }
}