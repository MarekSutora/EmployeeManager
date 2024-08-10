using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Position
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
    }
}
