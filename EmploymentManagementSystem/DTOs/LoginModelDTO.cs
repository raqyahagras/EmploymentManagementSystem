using System.ComponentModel.DataAnnotations;

namespace EmploymentManagementSystem.API.DTOs
{
    public class LoginModelDTO
    {

        [Required]//Email field Is Required
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
