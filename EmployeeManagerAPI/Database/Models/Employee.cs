using System;
using System.Collections.Generic;

namespace Database.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public required string IpAddress { get; set; }
        public required string IpCountryCode { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;
    }
}
