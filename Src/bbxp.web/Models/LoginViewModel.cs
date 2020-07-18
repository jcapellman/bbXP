using System.ComponentModel.DataAnnotations;

namespace bbxp.web.Models
{
    public class LoginViewModel
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string ErrorString { get; set; }
    }
}