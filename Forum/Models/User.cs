using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public int UserTypeID { get; set; }
        public UserType UserTypes { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
