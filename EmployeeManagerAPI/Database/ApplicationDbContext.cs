using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Surname)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.IpAddress)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.IpCountryCode)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(e => e.PositionId)
                .IsRequired();

            modelBuilder.Entity<Position>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Position>().HasData(
                new Position { Id = 1, Name = "Manager" },
                new Position { Id = 2, Name = "Developer" },
                new Position { Id = 3, Name = "QA" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "John",
                    Surname = "Doe",
                    BirthDate = new DateTime(1990, 1, 1),
                    IpAddress = "182.164.12.11",
                    IpCountryCode = "US",
                    PositionId = 1
                    },
                new Employee
                {
                    Id = 2,
                    Name = "Jane",
                    Surname = "Doe",
                    BirthDate = new DateTime(1995, 1, 1),
                    IpAddress = "182.164.12.11",
                    IpCountryCode = "SK",
                    PositionId = 2
                    },
                new Employee
                {
                    Id = 3,
                    Name = "Jack",
                    Surname = "Doe",
                    BirthDate = new DateTime(2000, 1, 1),
                    IpAddress = "182.164.12.11",
                    IpCountryCode = "CZ",
                    PositionId = 3
                }
                );
        }
    }
}
