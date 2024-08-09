using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class CreatePositionDto
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; } = string.Empty;
    }
}
