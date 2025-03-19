using System.ComponentModel.DataAnnotations;

namespace buildingLink.Models
{
    public class Driver
    {
        public int Id { get; set; } = 0;
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
         
    }
}
