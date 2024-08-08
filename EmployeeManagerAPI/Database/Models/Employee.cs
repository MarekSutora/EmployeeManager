using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Surname { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public required string IpAddress { get; set; }
        [Required]
        public required string IpCountryCode { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;
    }
}
