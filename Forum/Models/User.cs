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
        public string UserPassword { get; set; }
        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [BindNever]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime UserRegisteredDate { get; set; }
        public DateTime? UserBannedTime { get; set; }
        public string? UserBanReason { get; set; }
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
