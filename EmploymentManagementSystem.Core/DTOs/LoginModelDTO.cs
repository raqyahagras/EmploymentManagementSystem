using System.ComponentModel.DataAnnotations;

namespace EmploymentManagementSystem.Core.DTOs
{
    public class LoginModelDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthModel
    {

        public string UserID { get; set; }
        public string Message { get; set; }

        public string UserName { get; set; }

        public bool IsAuthenticated { get; set; }


        public string Email { get; set; }

        public List<string> Roles { get; set; }


        public string Token { get; set; }




    }

}
