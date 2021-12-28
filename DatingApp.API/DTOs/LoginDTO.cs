using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class LoginDTO
    {
        [Required]
        [StringLength(32)]
        public string Username {get; set;}
        [Required]
        public string Password {get; set;}
    }
}