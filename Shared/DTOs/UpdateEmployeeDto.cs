﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class UpdateEmployeeDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth date is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "IP address is required")]
        public string IpAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "IP country code is required")]
        public string IpCountryCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Position ID is required")]
        public int PositionId { get; set; }
    }
}
