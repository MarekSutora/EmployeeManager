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
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId);

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
                    IpAddress = "0.0.0.0",
                    IpCountryCode = "US",
                    PositionId = 1
                    },
                new Employee
                {
                    Id = 2,
                    Name = "Jane",
                    Surname = "Doe",
                    BirthDate = new DateTime(1995, 1, 1),
                    IpAddress = "1.1.1.1",
                    IpCountryCode = "SK",
                    PositionId = 2
                    },
                new Employee
                {
                    Id = 3,
                    Name = "Jack",
                    Surname = "Doe",
                    BirthDate = new DateTime(2000, 1, 1),
                    IpAddress = "2.2.2.2",
                    IpCountryCode = "CZ",
                    PositionId = 3
                }
                );
        }
    }
}
