using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentAddedTime { get; set; }
        public DateTime? CommentUpdatedTime { get; set; }
        public DateTime? CommentDeletedTime { get; set; }
        public bool IsActive { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
        public int TopicID { get; set; }
        public Topic Topics { get; set; }

    }
}
