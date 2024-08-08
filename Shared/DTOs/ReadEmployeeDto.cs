using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ReadEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public ReadPositionDto Position { get; set; } = new ReadPositionDto();
        public string IpAddress { get; set; } = string.Empty;
        public string IpCountryCode { get; set; } = string.Empty;
    }
}
