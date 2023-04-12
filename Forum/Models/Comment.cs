using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d/M/yyyy}")]
        public DateTime CommentAddedTime { get; set; }
        public DateTime? CommentUpdatedTime { get; set; }
        public DateTime? CommentDeletedTime { get; set; }
        public DateTime? CommentBannedTime { get; set; }
        public int? TotalCommentCount { get; set; }
        public int VotePlus { get; set; }
        public int VoteMinus { get; set; }
        public bool IsActive { get; set; }
        public int? CommentCount { get; set; }

        public string? CommentAuthor { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int TopicID { get; set; }
        public Topic Topics { get; set; }

    }
}
