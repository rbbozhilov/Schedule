

using Schedule.Data.Models;
using System.Data.Entity;

namespace Schedule.Data
{
    public class ScheduleDbContext : DbContext
    {

        public ScheduleDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<EmployeePositions> EmployeePositions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeePositions>()
                            .HasKey(ep => new { ep.EmployeeId, ep.PositionId });
        }


    }
}
