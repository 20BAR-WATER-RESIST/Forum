using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Nieprawidłowy adres e-mail")]
        public string Email { get; set; }
        public DateTime UserRegisteredDate { get; set; }    
        public DateTime? UserBannedTime { get; set; }
        public bool IsActive { get; set; }

        public int UserTypeID { get; set; }
        public UserType UserTypes { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
