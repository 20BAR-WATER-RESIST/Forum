using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class User
    {
        [Key]
        [BindNever]
        public int UserID { get; set; }
        [BindNever]
        public string UserName { get; set; }
        [Required]
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [BindNever]
        public DateTime UserRegisteredDate { get; set; }
        [BindNever]
        public DateTime? UserBannedTime { get; set; }
        [BindNever]
        public bool IsActive { get; set; }

        [BindNever]
        public int UserTypeID { get; set; }
        [BindNever]
        public UserType UserTypes { get; set; }
        [BindNever]
        public ICollection<Topic> Topics { get; set; }
        [BindNever]
        public ICollection<Comment> Comments { get; set; }
    }
}
