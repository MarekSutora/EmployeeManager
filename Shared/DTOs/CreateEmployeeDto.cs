using System.ComponentModel.DataAnnotations;


namespace Shared.DTOs
{
    public class CreateEmployeeDto
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
