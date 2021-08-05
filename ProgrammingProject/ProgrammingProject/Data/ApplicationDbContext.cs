using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Sitting> Sittings { get; set; }
        public DbSet<SittingType> SittingTypes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<ReservationSource> ReservationSources { get; set; }
        public DbSet<ReservationTable> ReservationTable { get; set; }
        public DbSet<Title> Titles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var today = DateTime.Now.Date;
            var tomorrow = today.AddDays(1);


            base.OnModelCreating(builder);
            builder.Entity<Title>().HasData(
                new Title { Id = 1, Name = "Mr" },
                new Title { Id = 2, Name = "Ms" },
                new Title { Id = 3, Name = "Mrs" },
                new Title { Id = 4, Name = "Miss" },
                new Title { Id = 5, Name = "Dr" },
                new Title { Id = 6, Name = "Prof" });
            builder.Entity<SittingType>().HasData(
                new SittingType { Id = 1, Name = "Breakfast"},
                new SittingType { Id = 2, Name = "Lunch"},
                new SittingType { Id = 3, Name = "Dinner"});
            builder.Entity<Restaurant>().HasData(
                new Restaurant { Id = 1, Name = "Petersham" });
            builder.Entity<Sitting>().HasData(
                new Sitting { Id = 1, Name = "Breakfast Today", Start = today.AddHours(7), End = today.AddHours(10), Capacity = 30, IsClosed = false, RestaurantId = 1, SittingTypeId = 1},
                new Sitting { Id = 2, Name = "Lunch Today", Start = today.AddHours(11), End = today.AddHours(14), Capacity = 30, IsClosed = false, RestaurantId = 1, SittingTypeId = 2 },
                new Sitting { Id = 3, Name = "Dinner Today", Start = today.AddHours(17), End = today.AddHours(20), Capacity = 30, IsClosed = false, RestaurantId = 1, SittingTypeId = 3 },
                new Sitting { Id = 4, Name = "Breakfast Tomorrow", Start = tomorrow.AddHours(7), End = tomorrow.AddHours(10), Capacity = 30, IsClosed = false, RestaurantId = 1, SittingTypeId = 1 },
                new Sitting { Id = 5, Name = "Lunch Tomorrow", Start = tomorrow.AddHours(11), End = tomorrow.AddHours(14), Capacity = 30, IsClosed = false, RestaurantId = 1, SittingTypeId = 2 },
                new Sitting { Id = 6, Name = "Dinner Tomorrow", Start = tomorrow.AddHours(17), End = tomorrow.AddHours(20), Capacity = 30, IsClosed = false, RestaurantId = 1, SittingTypeId = 3 });
            builder.Entity<ReservationSource>().HasData(
                new ReservationSource { Id = 1, Source = "Email/Phone" },
                new ReservationSource { Id = 2, Source = "Online" },
                new ReservationSource { Id = 3, Source = "Walk-In"});
            builder.Entity<ReservationStatus>().HasData(
                new ReservationStatus { Id = 1, Status = "Pending" },
                new ReservationStatus { Id = 2, Status = "Confirmed" },
                new ReservationStatus { Id = 3, Status = "Cancelled" },
                new ReservationStatus { Id = 4, Status = "Completed" });
        }
    }
}
