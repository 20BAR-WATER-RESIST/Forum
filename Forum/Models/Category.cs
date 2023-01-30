using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Topic> Topics { get; set; }
        //public ICollection<Comment> Comments { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
