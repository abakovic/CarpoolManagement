using CarpoolManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarpoolManagement.Data.CarpoolManagementContext
{
    public class CarpoolContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=ride.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carpool>()
                .HasIndex(x => x.Plates)
                .IsUnique();

            modelBuilder.Entity<EmployeeRide>()
                .HasKey(er => new { er.EmployeeId, er.RideId });
        }

        public DbSet<Carpool> Carpools { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RideSharing> Rides { get; set; }
        public DbSet<EmployeeRide> EmployeeRides { get; set; }
    }
}
