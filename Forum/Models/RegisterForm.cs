using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class RegisterForm
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(12, MinimumLength = 6)]
        public string RegisterUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(15, MinimumLength = 8)]
        public string RegisterPassword { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email adress")]
        public string RegisterEmail { get; set; }
    }
}
