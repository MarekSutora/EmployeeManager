﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class Position
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
