using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class LoginForm
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email adress")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Pass { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}
