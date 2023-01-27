using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Topic
    {
        [Key]
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public string? TopicTags { get; set; }
        public DateTime TopicAddedDate { get; set; }
        public DateTime? TopicUpdatedDate { get; set; }
        public DateTime? TopicDeletedDate { get; set; }
        public bool IsActive { get; set; }

        public int UserID { get; set; }
        public User Users { get; set; }
        public int CategoryID { get; set; }
        public Category Categories { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
