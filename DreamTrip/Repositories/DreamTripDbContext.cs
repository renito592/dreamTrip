using DreamTrip.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Repositories
{
    public class DreamTripDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripApplication> TripApplications { get; set; }
        public DreamTripDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany(u => u.TripApplications)
               .WithOne(app => app.User)
               .HasForeignKey(ua => ua.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasMany(j => j.TripApplications)
                .WithOne(app => app.Trip)
                .HasForeignKey(ua => ua.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TripApplication>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.TripApplications);

            modelBuilder.Entity<TripApplication>()
                .HasOne(ua => ua.Trip)
                .WithMany(j => j.TripApplications);

            modelBuilder.Entity<Trip>()
                .HasOne(j => j.User);
        }

    }
}
